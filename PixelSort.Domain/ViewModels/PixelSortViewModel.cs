using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PixelSort.Domain
{
    public class PixelSortViewModel : INotifyPropertyChanged
    {
        private readonly WriteableBitmapService bitmapService;
        private readonly IPixelService pixelService;

        private readonly TaskFactory uiTaskFactory;

        public ICommand GenerateRandomPixelsCommand { get; private set; }
        public ICommand SortPixelsCommand { get; private set; }


        private Visibility _imageVisibility;
        public Visibility ImageVisibility
        {
            get { return _imageVisibility; }
            set { _imageVisibility = value; OnPropertyChanged("ImageVisibility"); }
        }


        public PixelSortViewModel(
            TaskScheduler taskScheduler,
            WriteableBitmapService writeableBitmapService,
            IPixelService pixelService)
        {
            uiTaskFactory = new TaskFactory(taskScheduler);
            this.pixelService = pixelService;
            this.bitmapService = writeableBitmapService;

            GenerateRandomPixelsCommand = new PixelsCommandHandler(UpdateBackBufferWithRandomPixelData, () => !isGenerating);
            SortPixelsCommand = new PixelsCommandHandler(UpdateBackBufferWithSortedPixelData);

            ImageVisibility = Visibility.Hidden;
        }

        public BitmapSource WriteableBitmap 
        { 
            get => bitmapService.WriteableBitmap; 
        }

        public bool ArePixelsSorted() => pixelService.ArePixelsSorted();
        public bool ArePixelsEmpty() => pixelService.ArePixelsEmpty();
        private bool isGenerating;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler eventHandler = PropertyChanged;
            if (eventHandler != null)
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
        }


        public void UpdateBackBufferWithRandomPixelData()
        {
            isGenerating = true;
            var count = bitmapService.GetPixelCount();

            Task.Factory.StartNew(() =>
            {
                return pixelService.GenerateRandomPixelData(count);
            }).ContinueWith(t =>
            {
                uiTaskFactory.StartNew(() =>
                {
                    bitmapService.UpdateBackBuffer(t.Result);
                    isGenerating = false;
                    if (ImageVisibility == Visibility.Hidden)
                    {
                        ImageVisibility = Visibility.Visible;
                    }
                });

            });
        }

        public void UpdateBackBufferWithSortedPixelData()
        {
            if (pixelService.ArePixelsEmpty() || pixelService.ArePixelsSorted())
            {
                return;
            }

            Task.Factory.StartNew(() =>
            {
                return pixelService.GetSortedPixels();
            }).ContinueWith(t =>
            {
                uiTaskFactory.StartNew(() =>
                {
                    bitmapService.UpdateBackBuffer(t.Result);
                });

            });

        }
    }
}
