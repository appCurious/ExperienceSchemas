using System;
using System.Text.Json;
using Json.Schema;

namespace ExperienceSchemas
{
    class Program
    {
        static void Main(string[] args)
        {
            string carouselData = @"{
                ""widgetId"":""2dae289c-8eba-59eb-eba5-ae12704385e5"",
                ""widgetType"":""Carousel"",
                ""headerText"":""Carousel compare slide show"",
                ""layout"":""SlideShow"",
                ""items"":[
                    {
                        ""caption"":""I have a bullet point and a footnote"",
                        ""description"":""<p>CXH does not send in the quill indent css</p><ul><li>single bullet </li></ul><p>foot note<markup:footnote xmlns:markup=http://www.webcollage.com/objects/2008/1/markup><markup:rich-text>first footnote</markup:rich-text></markup:footnote></p>"",
                        ""link"": null,
                        ""linkLabel"":""Learn More"",
                        ""assetType"":""Video"",
                        ""videoLoop"":false,
                        ""asset"":{
                            ""assetType"":""Video"",
                            ""duration"":183.318005,
                            ""poster"":{
                                ""assetType"":""Image"",
                                ""url"":""https://alt-uat.syndigo.cloud/asset/a32e15eb-7a80-4ac9-9f47-61e56ad00c77/{0}.jpeg"",
                                ""alt"":"""",
                                ""assetId"":""be9b915d-fe7f-404a-807b-b1ea5340349f"",
                                ""originalHeight"":1529,
                                ""originalWidth"":1920,
                                ""availableWidths"":[
                                    1920,
                                    960,
                                    480,
                                    240
                                ],
                                ""mimeType"":""image/jpeg""
                            },
                            ""tracks"":[
                                {
                                    ""kind"":""captions"",
                                    ""srcLang"":""en"",
                                    ""url"":""https://alt-uat.syndigo.cloud/asset/c41cfdd3-7241-4e47-8f5e-bbe80e724004/original.vtt""
                                }
                            ],
                            ""sources"":[
                                {
                                    ""mimeType"":""video/hls"",
                                    ""url"":""https://alt-uat.syndigo.cloud/asset/af79624a-1739-4e11-b823-aa7e85b835ac/playlist.m3u8""
                                }
                            ],
                            ""assetId"":""af79624a-1739-4e11-b823-aa7e85b835ac"",
                            ""originalHeight"":480,
                            ""originalWidth"":640,
                            ""availableWidths"":[
                            
                            ],
                            ""mimeType"":""video/mp4""
                        },
                        ""colorOption"":""BlackOverWhite"",
                        ""itemPosition"":""Left"",
                        ""fontBackgroundColor"":""#ffffff"",
                        ""fontColor"":""#000000"",
                        ""imageBackgroundColor"":null,
                        ""fontOpacity"":100,
                        ""textWidth"":100,
                        ""textPosition"":""Center""
                    }
                ]
            }";
            JsonValidator carouselValidator = new CarouselValidator();
            JsonDocument jdoc = JsonDocument.Parse(carouselData);

            Console.WriteLine("Prepare to Validate");
            ValidationResults results = carouselValidator.validateData(jdoc.RootElement);
            
            Console.Write("isValid: ");
            Console.Write(results.IsValid);
            Console.WriteLine("");
            Console.Write("Result Messages: ");
            var message = results.Message != null ? results.Message.ToString() : "Valid JSON";
            Console.Write(message);
        }
    }
}
