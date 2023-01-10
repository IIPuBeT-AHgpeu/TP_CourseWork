using DlibDotNet;
using System.Drawing;
using System.Drawing.Imaging;
using Image = TP_CourseWork.Models.Image;

namespace TP_CourseWork.Services
{
    public class FacesByPicture : IStrategyByPicture
    {
        private FrontalFaceDetector faceDetector = Dlib.GetFrontalFaceDetector();
        public int GetObjectsCount(Image image)
        {
            int count = 0;

            using (var ms = new MemoryStream(image.Data))
            {
                using (var bitmap = (Bitmap)System.Drawing.Image.FromStream(ms))
                using (var png = new MemoryStream())
                {
                    bitmap.Save(png, ImageFormat.Png);
                    using (var matrix = Dlib.LoadPng<BgrPixel>(png.ToArray()))
                  
                    {
                        var dets = faceDetector.Operator(matrix);
                        count = dets.Count();
                    }
                }
            }

            return (count);
        }
    }
}
