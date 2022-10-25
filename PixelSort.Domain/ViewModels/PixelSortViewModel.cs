using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PixelSort.Domain
{
    /// <summary>
    /// Viewmodel class for MainWindow
    /// </summary>
    public class PixelSortViewModel : ViewModelBase
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

        /// <summary>
        /// The image source for the pixel image
        /// </summary>
        public BitmapSource WriteableBitmap 
        { 
            get => bitmapService.WriteableBitmap; 
        }

        public bool ArePixelsSorted() => pixelService.ArePixelsSorted();
        
        private bool _isRandomButtonEnabled;

        /// <summary>
        /// Represents wheather the random pixel button is enabled
        /// Random pixel button is disabled when the application is generating random pixels.
        /// </summary>
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

        /// <summary>
        /// Represents wheather the sort pixel button is enabled.
        /// Sort pixel button is diabled when the application is sorting the pixels.
        /// </summary>
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
        
        /// <summary>
        /// The visibility of the image on main window.
        /// Initially the image visibility is set to hidden.
        /// When back buffer is updated with random pixels, Visibility is set to visible.
        /// </summary>
        public Visibility ImageVisibility
        {
            get => _imageVisibility;
            set 
            {
                _imageVisibility = value;
                OnPropertyChanged("ImageVisibility");
            }
        }

        /// <summary>
        /// Updates the back buffer of the writable bitmap with randomly generated pixel data
        /// and sets the image visibility to visible if the image is hidden.
        /// </summary>
        public void UpdateBackBufferWithRandomPixelData()
        {
            IsRandomButtonEnabled = false;
            var count = bitmapService.GetPixelCount();

            Task.Factory.StartNew(() =>
            {
                return pixelService.GenerateRandomPixelData(count);
            }).ContinueWith(t =>
            {
                if (t.IsCompleted)
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
                }
                // TODO :: handle Faulted or Cancelled states
            });
        }


        /// <summary>
        /// Updates the back buffer of the writable bitmap with sorted pixels
        /// Does not update the back buffer if the pixels are empty or when pixels are already sorted.
        /// </summary>
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
                if (t.IsCompleted)
                {
                    uiTaskFactory.StartNew(() =>
                    {
                        bitmapService.UpdateBackBuffer(t.Result);
                        IsSortButtonEnabled = true;
                    });
                }
                // TODO :: handle Faulted or Cancelled states
            });

        }
    }
}
