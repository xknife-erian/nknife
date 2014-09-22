namespace Jeelu.SimplusPagePreviewer
{
    static public class ConstService
    {
        public const string PUBlISHDTD = " <!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">";
        public const string HTMLUTF8Head = "<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/><title>{0}</title></head>";
        //public const string CHANNELINDEXPAGEHEAD="<html><head><META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">";
        public const string HTMLUTF8END = "</html>";
        //public const string CHANNELINDEXPAGEEND = "</head></html>";
        public const string BAD = "HTTP/1.1 400 Bad Request\r\n";
        public const string OK = "HTTP/1.1 200 OK\r\n";
        public const string Redirect = "HTTP/1.1 301 Permanently Moved\r\n";
        public const string HTML = ".html";
        public const string UTF8 = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>";
        public const string INDEXSHTML = "index.shtml";

        public const int MaxCache = 1024*1024*10;
    }
}
