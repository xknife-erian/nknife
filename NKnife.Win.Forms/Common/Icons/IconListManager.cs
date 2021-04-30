using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace NKnife.Win.Forms.Common.Icons
{
    /// <summary>Maintains a list of currently added file extensions
    /// </summary>
    public class IconListManager
    {
        private readonly Hashtable _ExtensionList = new Hashtable();

        private readonly IconReader.IconSize _IconSize;

        /// <summary>will hold ImageList objects
        /// </summary>
        private readonly ArrayList _ImageLists = new ArrayList();

        /// <summary>flag, used to determine whether to create two ImageLists.
        /// </summary>
        private readonly bool _ManageBothSizes;

        /// <summary>Creates an instance of <c>IconListManager</c> that will add icons to a single <c>ImageList</c> using the
        /// specified <c>IconSize</c>.
        /// </summary>
        /// <param name="imageList"><c>ImageList</c> to add icons to.</param>
        /// <param name="iconSize">Size to use (either 32 or 16 pixels).</param>
        public IconListManager(ImageList imageList, IconReader.IconSize iconSize)
        {
            // Initialise the members of the class that will hold the image list we're
            // targeting, as well as the icon size (32 or 16)
            _ImageLists.Add(imageList);
            _IconSize = iconSize;
        }

        /// <summary>Creates an instance of IconListManager that will add icons to two <c>ImageList</c> types. The two
        /// image lists are intended to be one for large icons, and the other for small icons.
        /// </summary>
        /// <param name="smallImageList">The <c>ImageList</c> that will hold small icons.</param>
        /// <param name="largeImageList">The <c>ImageList</c> that will hold large icons.</param>
        public IconListManager(ImageList smallImageList, ImageList largeImageList)
        {
            //add both our image lists
            _ImageLists.Add(smallImageList);
            _ImageLists.Add(largeImageList);

            //set flag
            _ManageBothSizes = true;
        }

        /// <summary>Used internally, adds the extension to the hashtable, so that its value can then be returned.
        /// </summary>
        /// <param name="extension"><c>String</c> of the file's extension.</param>
        /// <param name="imageListPosition">Position of the extension in the <c>ImageList</c>.</param>
        private void AddExtension(string extension, int imageListPosition)
        {
            _ExtensionList.Add(extension, imageListPosition);
        }

        /// <summary>Called publicly to add a file's icon to the ImageList.
        /// </summary>
        /// <param name="filePath">Full path to the file.</param>
        /// <returns>Integer of the icon's position in the ImageList</returns>
        public int AddFileIcon(string filePath)
        {
            // Check if the file exists, otherwise, throw exception.
            if (!File.Exists(filePath)) throw new FileNotFoundException("File does not exist");

            // Split it down so we can get the extension
            string[] splitPath = filePath.Split(new[] {'.'});
            var extension = (string) splitPath.GetValue(splitPath.GetUpperBound(0));

            //Check that we haven't already got the extension, if we have, then
            //return back its index
            if (_ExtensionList.ContainsKey(extension.ToUpper()))
            {
                return (int) _ExtensionList[extension.ToUpper()]; //return existing index
            }
            // It's not already been added, so add it and record its position.
            int pos = ((ImageList) _ImageLists[0]).Images.Count; //store current count -- new item's index

            if (_ManageBothSizes)
            {
                //managing two lists, so add it to small first, then large
                ((ImageList) _ImageLists[0]).Images.Add(IconReader.GetFileIcon(filePath, IconReader.IconSize.Small, false));
                ((ImageList) _ImageLists[1]).Images.Add(IconReader.GetFileIcon(filePath, IconReader.IconSize.Large, false));
            }
            else
            {
                //only doing one size, so use IconSize as specified in _iconSize.
                ((ImageList) _ImageLists[0]).Images.Add(IconReader.GetFileIcon(filePath, _IconSize, false)); //add to image list
            }

            AddExtension(extension.ToUpper(), pos); // add to hash table
            return pos;
        }

        /// <summary>Clears any <c>ImageLists</c> that <c>IconListManager</c> is managing.
        /// </summary>
        public void ClearLists()
        {
            foreach (ImageList imageList in _ImageLists)
            {
                imageList.Images.Clear(); //clear current imagelist.
            }

            _ExtensionList.Clear(); //empty hashtable of entries too.
        }
    }
}