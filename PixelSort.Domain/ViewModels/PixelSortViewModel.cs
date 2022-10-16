using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PixelSort.Domain
{
    public class PixelSortViewModel : INotifyPropertyChanged
    {
        private readonly TaskFactory uiTaskFactory;
        private readonly WriteableBitmapService bitmapService;
        private readonly IPixelService pixelService;

        public ICommand GenerateRandomPixelsCommand { get; private set; }
        public ICommand SortPixelsCommand { get; private set; }

        public PixelSortViewModel(
            TaskScheduler taskScheduler,
            WriteableBitmapService writeableBitmapService,
            IPixelService pixelService)
        {
            uiTaskFactory = new TaskFactory(taskScheduler);
            this.pixelService = pixelService;
            this.bitmapService = writeableBitmapService;

            GenerateRandomPixelsCommand = new RelayCommand(UpdateBackBufferWithRandomPixelData);
            SortPixelsCommand = new RelayCommand(UpdateBackBufferWithSortedPixelData);

            ImageVisibility = Visibility.Hidden;
            IsRandomButtonEnabled = true;
            IsSortButtonEnabled = true;
        }

        public BitmapSource WriteableBitmap 
        { 
            get => bitmapService.WriteableBitmap; 
        }

        public bool ArePixelsSorted() => pixelService.ArePixelsSorted();
        
        private bool _isRandomButtonEnabled;
        public bool IsRandomButtonEnabled
        {
            get => _isRandomButtonEnabled;
            set
            {
                _isRandomButtonEnabled = value;
                OnPropertyChanged("IsRandomButtonEnabled");
            }
        }

        private bool _isSortButtonEnabled;
        public bool IsSortButtonEnabled
        {
            get => _isSortButtonEnabled;
            set
            {
                _isSortButtonEnabled = value;
                OnPropertyChanged("IsSortButtonEnabled");
            }
        }

        private Visibility _imageVisibility;
        public Visibility ImageVisibility
        {
            get => _imageVisibility;
            set 
            {
                _imageVisibility = value;
                OnPropertyChanged("ImageVisibility");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler eventHandler = PropertyChanged;
            if (eventHandler != null)
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateBackBufferWithRandomPixelData()
        {
            IsRandomButtonEnabled = false;
            var count = bitmapService.GetPixelCount();

            Task.Factory.StartNew(() =>
            {
                return pixelService.GenerateRandomPixelData(count);
            }).ContinueWith(t =>
            {
                uiTaskFactory.StartNew(() =>
                {
                    bitmapService.UpdateBackBuffer(t.Result);
                    IsRandomButtonEnabled = true;
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

            IsSortButtonEnabled = false;

            Task.Factory.StartNew(() =>
            {
                return pixelService.GetSortedPixels();
            }).ContinueWith(t =>
            {
                uiTaskFactory.StartNew(() =>
                {
                    bitmapService.UpdateBackBuffer(t.Result);
                    IsSortButtonEnabled = true;
                });

            });

        }
    }
}
