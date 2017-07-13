using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using System.Collections.ObjectModel;
using Windows.Storage.Pickers;
using Windows.Media.SpeechSynthesis;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;





// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NETRA
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        CameraCaptureUI captureUI = new CameraCaptureUI();
        StorageFile photo;
        IRandomAccessStream imageStream;
        public object ImageViewer { get; private set; }
        public object FileNameTextBlock { get; private set; }

        const string APIKEY = "a7997ca19683477587af5f1623e2dade";
        EmotionServiceClient emotionServiceClient = new EmotionServiceClient(APIKEY);
        Emotion[] emotionResult;

        public MainPage()
        {
            this.InitializeComponent();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
           
        }

        private async void takePhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);
                if (photo == null)
                {
                    //User cancelled photo
                    return;
                }
                else
                {
                    imageStream = await photo.OpenAsync(FileAccessMode.Read);
                    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(imageStream);
                    SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();
                    SoftwareBitmap softwareBitmapBGR8 = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
                    SoftwareBitmapSource bitmapSource = new SoftwareBitmapSource();
                    await bitmapSource.SetBitmapAsync(softwareBitmapBGR8);
                    image.Source = bitmapSource;


                }
            }
            catch
            {
                output.Text = "Error taking photo";

                
            }
        }

        private async void getEmotion_Click(object sender, RoutedEventArgs e)
        {
           
            
            try
            {
                emotionResult = await emotionServiceClient.RecognizeAsync(imageStream.AsStream());
                if (emotionResult != null)
                {
                    Scores score = emotionResult[0].Scores;
                    output.Text = "Your emotions are: \n" +
                        "Happiness: " + score.Happiness + "\n" +
                        "Sadness: " + score.Sadness + "\n" +
                        "Surprise: " + score.Surprise + "\n" +
                        "Fear: " + score.Fear + "\n" +
                        "Anger:" + score.Anger + "\n" +
                        "Contempt:" + score.Contempt + "\n" +
                        "Disgust:" + score.Disgust + "\n" +
                        "Neutral:" + score.Neutral + "\n";
                    if(score.Happiness> score.Sadness && score.Happiness > score.Surprise && score.Happiness > score.Fear && score.Happiness > score.Anger && score.Happiness > score.Contempt && score.Happiness>score.Disgust &&
                        score.Happiness>score.Neutral)
                    {
                        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                        SpeechSynthesisStream SynthesisStream = await synthesizer.SynthesizeTextToStreamAsync("You are Happy");


                        //set source and start playing in syn audio stream.
                        media.AutoPlay = true;
                        media.SetSource(SynthesisStream, SynthesisStream.ContentType);
                        media.Play();
                        media1.Play();

                    }
                    else if (score.Sadness > score.Happiness && score.Sadness > score.Surprise && score.Sadness > score.Fear  && score.Sadness > score.Anger && score.Sadness > score.Contempt && score.Sadness > score.Disgust &&
                        score.Sadness > score.Neutral)
                    {
                        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                        SpeechSynthesisStream SynthesisStream = await synthesizer.SynthesizeTextToStreamAsync("You are Sad");


                        //set source and start playing in syn audio stream.
                        media.AutoPlay = true;
                        media.SetSource(SynthesisStream, SynthesisStream.ContentType);
                        media.Play();
                        media2.Play();
                    }
                    else if (score.Surprise> score.Happiness && score.Surprise > score.Sadness && score.Surprise > score.Fear && score.Surprise > score.Anger &&  score.Surprise > score.Contempt && score.Surprise > score.Disgust &&
    score.Surprise > score.Neutral)
                    {
                        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                        SpeechSynthesisStream SynthesisStream = await synthesizer.SynthesizeTextToStreamAsync("You are Surprised");


                        //set source and start playing in syn audio stream.
                        media.AutoPlay = true;
                        media.SetSource(SynthesisStream, SynthesisStream.ContentType);
                        media.Play();
                        media4.Play();


                    }
                    else if (score.Fear > score.Happiness && score.Fear > score.Sadness && score.Fear>score.Surprise && score.Fear > score.Anger && score.Fear > score.Contempt && score.Fear > score.Disgust &&
    score.Fear > score.Neutral)
                    {
                        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                        SpeechSynthesisStream SynthesisStream = await synthesizer.SynthesizeTextToStreamAsync("You are Frightened");


                        //set source and start playing in syn audio stream.
                        media.AutoPlay = true;
                        media.SetSource(SynthesisStream, SynthesisStream.ContentType);
                        media.Play();
                        media5.Play();
                    }

                    else if (score.Anger > score.Happiness && score.Anger > score.Sadness && score.Anger > score.Fear && score.Anger > score.Surprise && score.Anger > score.Contempt && score.Anger > score.Disgust &&
    score.Anger > score.Neutral)
                    {
                        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                        SpeechSynthesisStream SynthesisStream = await synthesizer.SynthesizeTextToStreamAsync("You are Angry");


                        //set source and start playing in syn audio stream.
                        media.AutoPlay = true;
                        media.SetSource(SynthesisStream, SynthesisStream.ContentType);
                        media.Play();
                    }
                    else if (score.Contempt> score.Happiness && score.Contempt > score.Sadness && score.Contempt > score.Fear && score.Contempt > score.Anger && score.Contempt > score.Surprise && score.Contempt> score.Disgust &&
    score.Contempt > score.Neutral)
                    {
                        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                        SpeechSynthesisStream SynthesisStream = await synthesizer.SynthesizeTextToStreamAsync("You are Have feeling of contemped");


                        //set source and start playing in syn audio stream.
                        media.AutoPlay = true;
                        media.SetSource(SynthesisStream, SynthesisStream.ContentType);
                        media.Play();
                    }

                    else if (score.Disgust > score.Happiness && score.Disgust > score.Sadness && score.Disgust > score.Fear && score.Disgust > score.Anger && score.Disgust > score.Contempt && score.Disgust > score.Surprise &&
    score.Disgust > score.Neutral)
                    {
                        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                        SpeechSynthesisStream SynthesisStream = await synthesizer.SynthesizeTextToStreamAsync("You are feeling disgusting");


                        //set source and start playing in syn audio stream.
                        media.AutoPlay = true;
                        media.SetSource(SynthesisStream, SynthesisStream.ContentType);
                        media.Play();
                    }

                    else
                    {
                        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                        SpeechSynthesisStream SynthesisStream = await synthesizer.SynthesizeTextToStreamAsync("You are felling neutral");


                        //set source and start playing in syn audio stream.
                        media.AutoPlay = true;
                        media.SetSource(SynthesisStream, SynthesisStream.ContentType);
                        media.Play();
                        media3.Play();


                    }
                }
            }
            catch
            {
                SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                SpeechSynthesisStream SynthesisStream = await synthesizer.SynthesizeTextToStreamAsync("There is an Error Please Click the picture agaian");


                //set source and start playing in syn audio stream.
                media.AutoPlay = true;
                media.SetSource(SynthesisStream, SynthesisStream.ContentType);
                media.Play();

            }

        }

        private async void button3_Click(object sender, RoutedEventArgs e)
        {



            // Create FileOpenPicker instance    
            FileOpenPicker fileOpenPicker = new FileOpenPicker();

            // Set SuggestedStartLocation    
            fileOpenPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            // Set ViewMode    
            fileOpenPicker.ViewMode = PickerViewMode.Thumbnail;

            // Filter for file types. For example, if you want to open text files,  
            // you will add .txt to the list.  

            fileOpenPicker.FileTypeFilter.Clear();
            fileOpenPicker.FileTypeFilter.Add(".png");
            fileOpenPicker.FileTypeFilter.Add(".jpeg");
            fileOpenPicker.FileTypeFilter.Add(".jpg");
            fileOpenPicker.FileTypeFilter.Add(".bmp");

            // Open FileOpenPicker    
            StorageFile file = await fileOpenPicker.PickSingleFileAsync();
            if (file != null)
            {
                FileNameTextBlock = file.Name;
                // Open a stream    
                Windows.Storage.Streams.IRandomAccessStream fileStream =
                await file.OpenAsync(FileAccessMode.Read);

                // Create a BitmapImage and Set stream as source    
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.SetSource(fileStream);

                // Set BitmapImage Source    
                ImageViewer = bitmapImage;
            }

        }
    }

}


