using System;
using System.Collections.Generic;
using System.Text.Json;
using Newtonsoft.Json;


namespace ExperienceSchemas
{
    
    public class ExampleWidget
    {
        #nullable enable
        
        /// <summary>
        /// The Widget Id
        /// </summary>
        [JsonProperty("widgetId")]
        public Guid WidgetId { get; set; }
       
        /// <summary>
        /// The Type of Widget
        /// </summary>
        [JsonProperty("widgetType")]
        public string WidgetType { get; } = "ExampleWidget";

        /// <summary>
        /// Widget Header
        /// </summary>
        [JsonProperty("headerText")]
        public string? HeaderText { get; set; }
        
        /// <summary>
        /// The Layout
        /// </summary>
        [JsonProperty("layout")]
        public string? Layout { get; set; }
        
        /// <summary>
        /// The List of items
        /// </summary>
        [JsonProperty("items")]
        public List<ExampleWidgetItem>? Items { get; set; }

        public static ExampleWidget getExampleWidget ()
        {
            ExampleWidgetAsset asset = new ExampleWidgetAsset();
            asset.AssetId = Guid.NewGuid().ToString();
            asset.AssetType = "Image";
            asset.OriginalHeight = 100;
            asset.OriginalWidth = 100;
            asset.AvailableWidths = new  List<int>(2);
            asset.AvailableWidths.Add(100);
            asset.AvailableWidths.Add(50);
            asset.Alt = "Nothing to see here";
            asset.Url = "https://nowhere.com/assets/none.jpg";
            asset.MimeType = "image/jpg";
        
            ExampleWidgetItem item = new ExampleWidgetItem();
            item.Asset = asset;
            item.AssetType = "Image";
            item.Caption = "Example Caption";
            item.Description = "Example Description";
            item.VideoLoop = false;

                        
            ExampleWidget example = new ExampleWidget();
            example.WidgetId = Guid.NewGuid();
            example.HeaderText = "Example Header Text";
            example.Layout = "UpDown";
            example.Items = new List<ExampleWidgetItem>();
            example.Items.Add(item);

            return example;
        }

    }

    public class ExampleWidgetAsset
    {
        #nullable enable

        /// <summary>
        /// The Asset Type
        /// </summary>
        [JsonProperty("assetType")] 
        public string AssetType { get; set; } = "Image";

        /// <summary>
        /// The Asset Id
        /// </summary>
        [JsonProperty("assetId")]
        public string? AssetId { get; set; }

        /// <summary>
        /// The Original Height
        /// </summary>
        [JsonProperty("originalHeight")]
        public int? OriginalHeight { get; set; }
        
        /// <summary>
        /// The Original Width
        /// </summary>
        [JsonProperty("originalWidth")]
        public int? OriginalWidth { get; set; }

        /// <summary>
        /// Gets or sets generated widths during asset preparations
        /// </summary>
        [JsonProperty("availableWidths")]
        public List<int> AvailableWidths { get; set; } = new List<int>();
        
        /// <summary>
        /// gets or sets string representation of Asset's mime type
        /// </summary>
        [JsonProperty("mimeType")]
        public string? MimeType { get; set; }

        [JsonProperty("alt")]
        public string? Alt { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }
    }

    public class ExampleWidgetItem
    {
        /// <summary>
        /// The Caption
        /// </summary>
        [JsonProperty("caption")]
        public string? Caption { get; set; }

        /// <summary>
        /// The Description
        /// </summary>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <summary>
        /// The Link to more information
        /// </summary>
        [JsonProperty("link")]
        public string? Link { get; set; }

        /// <summary>
        /// used by aria-label or alt
        /// </summary>
        [JsonProperty("linkLabel")]
        public string? LinkLabel { get; set; }

        /// <summary>
        /// The Asset Type
        /// </summary>
        [JsonProperty("assetType")]
        public string? AssetType { get; set; }

        /// <summary>
        /// Video Loop?
        /// </summary>
        [JsonProperty("videoLoop")]
        public bool VideoLoop { get; set; }

        /// <summary>
        /// The Asset
        /// </summary>
        [JsonProperty("asset")]
        public ExampleWidgetAsset? Asset { get; set; }

        /// <summary>
        /// The Color Option
        /// </summary>
        [JsonProperty("colorOption")]
        public string? ColorOption { get; set; }

        /// <summary>
        /// The Item Position
        /// </summary>
        [JsonProperty("itemPosition")]
        public string? ItemPosition { get; set; }

        /// <summary>
        /// The Font Background Color
        /// </summary>
        [JsonProperty("fontBackgroundColor")]
        public string? FontBackgroundColor { get; set; }

        /// <summary>
        /// The Font Color
        /// </summary>
        [JsonProperty("fontColor")]
        public string? FontColor { get; set; }

        /// <summary>
        /// The Image Background Color
        /// </summary>
        [JsonProperty("imageBackgroundColor")]
        public string? ImageBackgroundColor { get; set; }

        /// <summary>
        /// The Font Opacity
        /// </summary>
        [JsonProperty("fontOpacity")]
        public int? FontOpacity { get; set; }

        /// <summary>
        /// The Text Width
        /// </summary>
        [JsonProperty("textWidth")]
        public int? TextWidth { get; set; }
        
        /// <summary>
        /// The Text Position
        /// </summary>
        [JsonProperty("textPosition")]
        public string? TextPosition { get; set; }
    }
}