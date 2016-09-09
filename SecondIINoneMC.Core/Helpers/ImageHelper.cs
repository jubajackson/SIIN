#region using directives
using System;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Collections;
using System.Web;
#endregion

namespace SecondIINoneMC.Core.Helpers
{
    public static class ImageHelper
    {
        #region Constants

        #region Private Constants
        private const Int32 _maximumImageSideSize = 500;
        #endregion

        #endregion

        #region Methods

        #region Static Public Methods


        /// <summary>
        /// Resizes all image contained within a particular directory and its sub directories.
        /// </summary>
        /// <author>Juba Jitu Jackson</author>
        /// <date>01/04/10</date>
        /// <param name="path">Directory Path that contains images and sub directories with images to be resized.</param>
        /// <returns>void</returns>
        public static void ResizeImagesInDirectoryTree(String path)
        {
            foreach (String directory in Directory.GetDirectories(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directory);

                ResizeImagesInDirectory(directoryInfo.FullName);

                ResizeImagesInDirectoryTree(directoryInfo.FullName);
            }
        }

        /// <summary>
        /// Resizes all image contained within a particular directory.
        /// </summary>
        /// <author>Juba Jitu Jackson</author>
        /// <date>01/04/10</date>
        /// <param name="path">Directory Path that contains the images to be resized.</param>
        /// <returns>void</returns>
        public static void ResizeImagesInDirectory(String path)
        {
            foreach (String image in Directory.GetFiles(path))
            {
                ResizeImage(image);
            }
        }

        /// <summary>
        /// Resizes an image.
        /// </summary>
        /// <author>Juba Jitu Jackson</author>
        /// <date>01/04/10</date>
        /// <param name="path">Image Path</param>
        /// <returns>void</returns>
        public static void ResizeImage(String path)
        {
            FileInfo image = new FileInfo(path);

            if (image.Exists)
            {
                Stream stream = new FileStream(image.FullName, FileMode.OpenOrCreate, FileAccess.Read);

                String newFileName = image.Directory.FullName + "\\" + System.Guid.NewGuid() + image.Extension.ToLower();

                if (image.Extension.ToLower() == ".jpg" || image.Extension.ToLower() == ".gif")
                {
                    Image existingImage = Image.FromStream(stream);

                    if (existingImage.Width > _maximumImageSideSize || existingImage.Height > _maximumImageSideSize)
                    {
                        ResizeImageFromStream(newFileName, _maximumImageSideSize, stream);
                        FileHelper.DeleteFile(image.FullName);
                    }
                }
            }
        }

        /// <summary>
        /// Resizes an image and returns the new image width and height by way of output parameters.
        /// </summary>
        /// <author>Juba Jitu Jackson</author>
        /// <date>01/04/10</date>
        /// <param name="buffer">Stream Buffer that contains the image.</param>
        /// <param name="maxSideSize">The maximum size that any one side of the image can be.</param>
        /// <param name="width">Output parameter containing the new width.</param>
        /// <param name="height">Output parameter containing the new height.</param>
        /// <returns>void</returns>
        public static void GetResizedImageDemensions(Stream buffer, Int32 maxSideSize, out Int32 width, out Int32 height)
        {
            Image imgInput = Image.FromStream(buffer);

            //get image original width and height 
            Int32 intOldWidth = imgInput.Width;
            Int32 intOldHeight = imgInput.Height;

            //determine if landscape or portrait 
            Int32 intMaxSide;

            if (intOldWidth >= intOldHeight)
            {
                intMaxSide = intOldWidth;
            }
            else
            {
                intMaxSide = intOldHeight;
            }

            if (intMaxSide > maxSideSize)
            {
                //set new width and height 
                double dblCoef = maxSideSize / (double)intMaxSide;
                width = Convert.ToInt32(dblCoef * intOldWidth);
                height = Convert.ToInt32(dblCoef * intOldHeight);
            }
            else
            {
                width = intOldWidth;
                height = intOldHeight;
            }

            //release used resources 
            imgInput.Dispose();
            buffer.Close();
        }


        /// <summary>
        /// Resizes Big Images From a stream
        /// </summary>
        /// <author>Juba Jitu Jackson</author>
        /// <date>12/09/08</date>
        /// <param name="maxSideSize">Maximum size that an image can have.</param>
        /// <param name="buffer">Image stream.</param>
        /// <returns>void</returns>
        public static void ResizeImageFromStream(Int32 maxSideSize, Stream buffer)
        {
            Int32 newWidth;
            Int32 newHeight;
            Image bufferImage = Image.FromStream(buffer);

            //get image original width and height 
            Int32 bufferImageWidth = bufferImage.Width;
            Int32 bufferImageHeight = bufferImage.Height;

            //determine if landscape or portrait 
            Int32 bufferImageMaxSideSize;

            if (bufferImageWidth >= bufferImageHeight)
            {
                bufferImageMaxSideSize = bufferImageWidth;
            }
            else
            {
                bufferImageMaxSideSize = bufferImageHeight;
            }


            if (bufferImageMaxSideSize > maxSideSize)
            {
                //set new width and height 
                double coefficient = maxSideSize / (double)bufferImageMaxSideSize;
                newWidth = Convert.ToInt32(coefficient * bufferImageWidth);
                newHeight = Convert.ToInt32(coefficient * bufferImageHeight);
            }
            else
            {
                newWidth = bufferImageWidth;
                newHeight = bufferImageHeight;
            }
        }

        /// <summary>
        /// Resizes Big Images From a stream
        /// </summary>
        /// <author>Juba Jitu Jackson</author>
        /// <date>12/09/08</date>
        /// <param name="newImagePath">Path to save the resized image to.</param>
        /// <param name="maxSideSize">Maximum size that an image can have.</param>
        /// <param name="buffer">Image stream.</param>
        /// <returns>void</returns>
        public static void ResizeImageFromStream(string newImagePath, Int32 maxSideSize, Stream buffer)
        {
            Int32 newWidth;
            Int32 newHeight;
            Image bufferImage = Image.FromStream(buffer);

            //Determine image format 
            ImageFormat fmtImageFormat = bufferImage.RawFormat;

            //get image original width and height 
            Int32 bufferImageWidth = bufferImage.Width;
            Int32 bufferImageHeight = bufferImage.Height;

            //determine if landscape or portrait 
            Int32 bufferImageMaxSideSize;

            if (bufferImageWidth >= bufferImageHeight)
            {
                bufferImageMaxSideSize = bufferImageWidth;
            }
            else
            {
                bufferImageMaxSideSize = bufferImageHeight;
            }


            if (bufferImageMaxSideSize > maxSideSize)
            {
                //set new width and height 
                double coefficient = maxSideSize / (double)bufferImageMaxSideSize;
                newWidth = Convert.ToInt32(coefficient * bufferImageWidth);
                newHeight = Convert.ToInt32(coefficient * bufferImageHeight);
            }
            else
            {
                newWidth = bufferImageWidth;
                newHeight = bufferImageHeight;
            }
            //create new bitmap 
            Bitmap resizedImage = new Bitmap(bufferImage, newWidth, newHeight);

            //save bitmap to disk 
            resizedImage.Save(newImagePath, fmtImageFormat);

            //release used resources 
            bufferImage.Dispose();
            resizedImage.Dispose();
            buffer.Close();
        }

        #endregion

        #endregion
    }
}
