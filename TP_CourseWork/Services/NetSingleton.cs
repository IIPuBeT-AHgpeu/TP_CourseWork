namespace TP_CourseWork.Services
{
    public class NetSingleton
    {
        private static volatile FacesByPicture instance;
        private static object syncRoot = new Object();

        private NetSingleton() { }

        public static FacesByPicture Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new FacesByPicture();
                    }
                }

                return instance;
            }
        }
    }
}
