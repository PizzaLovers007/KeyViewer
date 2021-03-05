namespace KeyViewer
{
    internal class Config
    {
        private static Config _instance;
        public static Config Instance {
            get {
                if (_instance == null) {
                    _instance = new Config();
                }
                return _instance;
            }
        }

        public double WindowX { get; set; } = 50;
        public double WindowY { get; set; } = 50;
        public double KeySize { get; set; } = 100;

        private Config() { }
    }
}
