using System.ComponentModel.DataAnnotations;

namespace TP_CourseWork.Models
{
    public class Image
    {
        #region Properties

        /// <summary>
        /// The image data.
        /// </summary>
        [Required]
        public byte[] Data
        {
            get;
            set;
        }

        #endregion
    }
}
