using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScpiKnife.Parser
{
/**
 * The <code>SCPIParser</code> is a general purpose command parser for
 * SCPI-style commands.
 *
 * Refer to
 * <a href="http://en.wikipedia.org/wiki/Standard_Commands_for_Programmable_Instruments">Standard
 * Commands for Programmable Instruments</a> on Wikipedia for more information
 * about SCPI.
 *
 * <h2>Usage</h2>
 *
 * <p>
 * To the {@link SCPIParser} class can be used in two ways. First, you can use
 * the class as-is by calling {@link addHandler} to add handlers to
 * {@link SCPIParser} objects.</p>
 *
 * <p>
 * The preferred usage is to extend the {@link SCPIParser} class and register
 * handlers in the constructor of the extending class. New handlers are
 * registered by calling the {@link #addHandler addHandler} method and
 * specifying the full, unabbreviated SCPI command path and an
 * {@link SCPICommandHandler}. Command handlers must implement the
 * {@link SCPICommandHandler} functional interface. For example,</p>
 *
 * <h3>Example Usage</h3>
 * <pre>
 * {@code class SimpleSCPIParser extends SCPIParser {
 *    public SimpleSCPIParser() {
 *      addHandler("*IDN?", this::IDN);
 *    }
 *
 *    string IDN(string[] args) {
 *          return "Simple SCPI Parser";
 *    }
 * }
 *
 * SimpleSCPIParser myParser = new SimpleSCPIParser();
 * for (string result : myParser.accept("*IDN?")) {
 *   System.out.println(result);
 * }}
 * </pre>
 *
 * <p>
 * The parser will correctly interpret chained commands of the form
 * <code>*IDN?;MEAS:VOLT:DC?;AC?</code>. In this case, three commands would be
 * processed, where the readonly command <code>MEAS:VOLT:AC?</code> would be
 * correctly interpreted.</p>
 *
 * <h2>Performance Considerations</h2>
 * <p>
 * By default, the SCPIParser caches
 * {@link #getCacheSizeLimit getCacheSizeLimit()} number of queries, greatly
 * speeding execution of <em>repeated</em> queries (default is 20). This
 * optimizes performance when the parser accepts many <em>identical</em>
 * queries. (Only the parsed version of queries is cached. Results are computed
 * for each call to {@link #accept accept(string query)}.)</p>
 *
 * <p>
 * Commands containing argument values are not cached by default. (For example,
 * queries that send data to the parser.) This prevents caching queries that are
 * not expected to be repeated often. However, if only a small number of queries
 * containing identical argument values can be expected, then caching can be
 * enabled by calling
 * {@link #setCacheQueriesWithArguments setCacheQueriesWithArguments(true)}.</p>
 *
 */
public class SCPIParser {

    private readonly Dictionary<ScpiPath, IScpiCommandHandler> handlers = new Dictionary<>();
    private readonly Dictionary<string, string> shortToLongCMD = new Dictionary<>();
    private readonly Dictionary<string, List<ScpiCommandCaller>> acceptCache = new Dictionary<>();
    private readonly Dictionary<string, LongAdder> acceptCacheKeyFrequency = new Dictionary<>();
    private static readonly Pattern tokenPatterns;
    private static readonly Pattern upperMatch;
    private static readonly IScpiCommandHandler nullCMDHandler;
    private static int MAX_CACHE_SIZE = 20;
    private static bool CACHE_QUERIES_WITH_ARGUMENTS = false;

    static SCPIParser() {
        tokenPatterns = buildLexer();
        upperMatch = Pattern.compile("[A-Z_*]+");
        nullCMDHandler = (string[] args) -> null;
    }

    public SCPIParser() {
    }

    /**
     * Adds a <code>SCPICommandHandler</code> for a specified SCPI path.
     *
     * @param path an absolute SCPI path
     * @param handler the method to associate with the path
     */
    public void addHandler(string path, IScpiCommandHandler handler) {
        ScpiPath scpipath = new ScpiPath(path);
        Iterator<string> elements = scpipath.Iterator();

        while (elements.hasNext()) {
            string element = getNonQueryPathElement(elements.next());
            Matcher matcher = upperMatch.matcher(element);
            if (matcher.find()) {
                shortToLongCMD.put(matcher.group(), element);
            }
        }
        handlers.put(scpipath, handler);
    }
    
    private bool isQuery(string input) {
        char lastChar = input.charAt(input.length() - 1);
        return (lastChar == '?');
    }
    
    private string getNonQueryPathElement(string input) {
        if (!isQuery(input)) {
            return input;
        } else {
            return input.substring(0, input.length() - 1);
        }
    }

    /**
     * Accepts query input and returns the results of query processing.
     *
     * Each element in the query is returned as a string, or null if no result
     * was returned by the handler.
     *
     * @param query a string containing input to the parser
     * @return returns an array containing the result of each command contained
     * in the query (may contain null)
     * @throws com.scpi.parser.SCPIParser.SCPIMissingHandlerException this
     * exception may be thrown if the query refers to an unmapped function or
     * contains an error. The caller should handle this exception.
     */
    public string[] accept(string query)  {
        List<ScpiCommandCaller> commands;
        if (MAX_CACHE_SIZE > 0 && acceptCache.containsKey(query)) {
            commands = acceptCache.get(query);
            acceptCacheKeyFrequency.computeIfPresent(query, (k, v) -> {
                v.increment();
                return v;
            });
        } else {
            List<ScpiToken> tokens = lex(query);
            commands = parse(tokens);
            bool commandsContainsArgument = false;
            if (!CACHE_QUERIES_WITH_ARGUMENTS) {
                for (ScpiToken token : tokens) {
                    if (token.tokenType == ScpiTokenTypeaa.ARGUMENT) {
                        commandsContainsArgument = true;
                        break;
                    }
                }
            }
            if (MAX_CACHE_SIZE > 0 && !commandsContainsArgument) {
                synchronized (this) {
                    if (acceptCache.size() > MAX_CACHE_SIZE - 1) {
                        string lowestFreqCmd = null;
                        Integer lowestFrequency = Integer.MAX_VALUE;
                        for (string key : acceptCacheKeyFrequency.keySet()) {
                            Integer count = acceptCacheKeyFrequency.get(key).intValue();
                            if (count < lowestFrequency) {
                                lowestFrequency = count;
                                lowestFreqCmd = key;
                            }
                        }
                        acceptCacheKeyFrequency.remove(lowestFreqCmd);
                        acceptCache.remove(lowestFreqCmd);
                    }
                    acceptCache.put(query, commands);
                    acceptCacheKeyFrequency.computeIfAbsent(query, k -> {
                        LongAdder longAdder = new LongAdder();
                        longAdder.increment();
                        return longAdder;
                    });
                }
            }
        }
        string[] results = new string[commands.size()];
        int index = 0;
        for (ScpiCommandCaller cmd : commands) {
            results[index++] = cmd.Execute();
        }
        return results;
    }

    /* (non-Javadoc)
     * This command should not be used in production code.
     * It returns a reference to the internal key cache frequency map and
     * may be used for testing and optimization purposes.
     * 
     * It is <b>not safe</b> to modify the contents of this map.
     * @return a reference to the internal cache frequency map.
     */
    private Dictionary<string, LongAdder> getCacheFrequency() {
        return acceptCacheKeyFrequency;
    }

    /**
     * If set to true, then queries containing argument values will be cached.
     * This can negatively impact performance if many unique queries are sent to
     * the parser.
     *
     * @param newValue desired caching state for queries that contain arguments.
     */
    public void setCacheQueriesWithArguments(bool newValue) {
        CACHE_QUERIES_WITH_ARGUMENTS = newValue;
    }

    /**
     *
     * @return the current state of the caching queries containing arguments.
     */
    public bool isCacheQueriesWithArguments() {
        return CACHE_QUERIES_WITH_ARGUMENTS;
    }

    /**
     * Sets the desired cache size limit (number of unique queries). Setting the
     * size to 0 will effectively disable caching. Negative values are
     * interpreted as 0.
     *
     * @param newSize the desired cache size
     */
    public void setCacheSizeLimit(int newSize) {
        MAX_CACHE_SIZE = (newSize >= 0) ? newSize : 0;
    }

    /**
     *
     * @return current cache size limit
     */
    public int getCacheSizeLimit() {
        return MAX_CACHE_SIZE;
    }

    private List<ScpiCommandCaller> parse(List<ScpiToken> tokens) {
        readonly List<ScpiCommandCaller> commands = new ArrayList<>();
        readonly ScpiPath activePath = new ScpiPath();
        readonly ArrayList<string> arguments = new ArrayList<>();
        bool inCommand = false;
        for (ScpiToken token : tokens) {
            switch (token.tokenType) {
                case COMMAND:
                    // normalize all commands to long-version
                    bool isQuery = isQuery(token.data);
                    string queryKey = getNonQueryPathElement(token.data);
                    string longCmd = shortToLongCMD.get(queryKey);
                    longCmd = (!isQuery) ? longCmd : longCmd + "?";
                    activePath.append((null == longCmd) ? token.data : longCmd);
                    inCommand = true;
                    break;
                case ARGUMENT:
                case QUOTEDstring:
                    arguments.add(token.data);
                    break;
                case COLON:
                    if (!inCommand) {
                        activePath.Clear();
                    }
                    break;
                case SEMICOLON:
                    // try to handle the current path
                    IScpiCommandHandler activeHandler = handlers.get(activePath);
                    if (null != activeHandler) {
                        commands.add(new ScpiCommandCaller(activeHandler, arguments.toArray(new string[arguments.size()])));
                    } else {
                        commands.add(new ScpiCommandCaller(nullCMDHandler, new string[]{}));
                        throw new ScpiMissingHandlerException(activePath.Tostring());
                    }
                    arguments.clear();
                    inCommand = false;
                    activePath.Strip();
                    break;
                case NEWLINE:
                case WHITESPACE:
                default:
                    break;
            }
        }
        return commands;
    }

   
    private List<ScpiToken> lex(string input) {
        ArrayList<ScpiToken> tokens = new ArrayList<>();

        // see optimization note for "tokenTypes" in SCPITokenType enum
        readonly ScpiTokenTypeaa[] tokenTypes = ScpiTokenTypeaa.tokenTypes;

        Matcher matcher = tokenPatterns.matcher(input);
        ScpiTokenTypeaa prevTokenType = ScpiTokenTypeaa.WHITESPACE;
        while (matcher.find()) {
            for (ScpiTokenTypeaa tokenType : tokenTypes) {
                string group = matcher.group(tokenType.name());
                if (group != null) {
                    switch (tokenType) {
                        case QUOTEDstring:
                            group = group.substring(1, group.length() - 1);
                        case COMMAND:
                        // fall through
                        case ARGUMENT:
                            ScpiTokenTypeaa typeToAdd = tokenType;
                            if (prevTokenType == ScpiTokenTypeaa.COMMAND) {
                                typeToAdd = ScpiTokenTypeaa.ARGUMENT;
                            }
                            tokens.add(new ScpiToken(typeToAdd, group));
                            prevTokenType = ScpiTokenTypeaa.COMMAND;
                            break;
                        case COLON:
                        // fall through
                        case SEMICOLON:
                            if (tokenType != prevTokenType) {
                                tokens.add(new ScpiToken(tokenType, null));
                                prevTokenType = tokenType;
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                }
            }
        }
        if (prevTokenType != ScpiTokenTypeaa.SEMICOLON) {
            tokens.add(new ScpiToken(ScpiTokenTypeaa.SEMICOLON, null));
        }
        return tokens;
    }

    private static Pattern buildLexer() {
        StringBuilder tokenPatternsBuffer = new StringBuilder();
        for (ScpiTokenTypeaa tokenType : ScpiTokenTypeaa.values()) {
            tokenPatternsBuffer.append(string.format("|(?<%s>%s)", tokenType.name(), tokenType.pattern));
        }
        return Pattern.compile(tokenPatternsBuffer.substring(1));
    }

    public class ScpiToken {

        public ScpiTokenTypeaa tokenType;
        public string data;

        ScpiToken(ScpiTokenTypeaa tokenType, string data) {
            this.tokenType = tokenType;
            this.data = data;
        }
    }

    /**
     * Interface to define a handler for a SCPI command. Implementations of this
     * interface are passed to the {@link #addHandler addHandler} method. Refer to the
     * {@code SCPIParser} class documentation for an example.
     */
    public interface IScpiCommandHandler {

        string handle(string[] args);
    }

    /**
     * Base class for SCPI-related exceptions
     */

    public class ScpiException : Exception
    {

        public ScpiException(string value) 
            : base(value)
        {
        }

    }

/**
     * An exception that may be raised while parsing a malformed-query, or query
     * that refers to an undefined command.
     */
    public class ScpiMissingHandlerException : ScpiException {

        private ScpiMissingHandlerException(string value) :base(value){
        }

    }

}
}