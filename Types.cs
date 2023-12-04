using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ExtractProducts;

public partial class PremiumGalleryImgContentCollection
{
    [JsonProperty("premium_gallery_img_content")]
    public PremiumGalleryImgContent[] PremiumGalleryImgContentPremiumGalleryImgContent { get; set; }
}

public partial class PremiumGalleryImgContent
{
    [JsonProperty("premium_gallery_img_name")]
    public string PremiumGalleryImgName { get; set; }

    [JsonProperty("premium_gallery_img_category")]
    public string PremiumGalleryImgCategory { get; set; }

    [JsonProperty("_id")]
    public string Id { get; set; }

    [JsonProperty("premium_gallery_img")]
    public PremiumGalleryImg PremiumGalleryImg { get; set; }

    [JsonProperty("premium_gallery_link_whole")]
    public string PremiumGalleryLinkWhole { get; set; }

    [JsonProperty("premium_gallery_img_link")]
    public PremiumGalleryImgLink PremiumGalleryImgLink { get; set; }

    [JsonProperty("premium_gallery_image_cell")]
    public PremiumGalleryImageCell PremiumGalleryImageCell { get; set; }

    [JsonProperty("premium_gallery_image_cell_tablet_extra")]
    public PremiumGalleryImage PremiumGalleryImageCellTabletExtra { get; set; }

    [JsonProperty("premium_gallery_image_cell_tablet")]
    public PremiumGalleryImage PremiumGalleryImageCellTablet { get; set; }

    [JsonProperty("premium_gallery_image_cell_mobile")]
    public PremiumGalleryImage PremiumGalleryImageCellMobile { get; set; }

    [JsonProperty("premium_gallery_image_vcell")]
    public PremiumGalleryImageCell PremiumGalleryImageVcell { get; set; }

    [JsonProperty("premium_gallery_image_vcell_tablet_extra")]
    public PremiumGalleryImage PremiumGalleryImageVcellTabletExtra { get; set; }

    [JsonProperty("premium_gallery_image_vcell_tablet")]
    public PremiumGalleryImage PremiumGalleryImageVcellTablet { get; set; }

    [JsonProperty("premium_gallery_image_vcell_mobile")]
    public PremiumGalleryImage PremiumGalleryImageVcellMobile { get; set; }

    [JsonProperty("premium_gallery_video")]
    public string PremiumGalleryVideo { get; set; }

    [JsonProperty("premium_gallery_video_type")]
    public object PremiumGalleryVideoType { get; set; }

    [JsonProperty("premium_gallery_video_url")]
    public object PremiumGalleryVideoUrl { get; set; }

    [JsonProperty("premium_gallery_video_self")]
    public object PremiumGalleryVideoSelf { get; set; }

    [JsonProperty("premium_gallery_video_self_url")]
    public object PremiumGalleryVideoSelfUrl { get; set; }

    [JsonProperty("premium_gallery_video_controls")]
    public object PremiumGalleryVideoControls { get; set; }

    [JsonProperty("premium_gallery_video_mute")]
    public object PremiumGalleryVideoMute { get; set; }

    [JsonProperty("premium_gallery_video_loop")]
    public object PremiumGalleryVideoLoop { get; set; }

    [JsonProperty("download_button")]
    public object DownloadButton { get; set; }

    [JsonProperty("privacy_mode")]
    public object PrivacyMode { get; set; }

    [JsonProperty("premmium_gallery_img_info")]
    public string PremmiumGalleryImgInfo { get; set; }

    [JsonProperty("premium_gallery_img_desc")]
    public string PremiumGalleryImgDesc { get; set; }

    [JsonProperty("premium_gallery_img_link_type")]
    public string PremiumGalleryImgLinkType { get; set; }

    [JsonProperty("premium_gallery_img_existing")]
    public object PremiumGalleryImgExisting { get; set; }

    [JsonProperty("premium_gallery_lightbox_whole")]
    public string PremiumGalleryLightboxWhole { get; set; }
}

public partial class PremiumGalleryImageCell
{
    [JsonProperty("unit")]
    public string Unit { get; set; }

    [JsonProperty("size")]
    public long Size { get; set; }

    [JsonProperty("sizes")]
    public object[] Sizes { get; set; }
}

public partial class PremiumGalleryImage
{
    [JsonProperty("unit")]
    public string Unit { get; set; }

    [JsonProperty("size")]
    public string Size { get; set; }

    [JsonProperty("sizes")]
    public object[] Sizes { get; set; }
}

public partial class PremiumGalleryImg
{
    [JsonProperty("url")]
    public Uri Url { get; set; }

    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("size")]
    public string Size { get; set; }

    [JsonProperty("alt")]
    public string Alt { get; set; }

    [JsonProperty("source")]
    public string Source { get; set; }
}

public partial class PremiumGalleryImgLink
{
    [JsonProperty("url")]
    public Uri Url { get; set; }

    [JsonProperty("is_external")]
    public string IsExternal { get; set; }

    [JsonProperty("nofollow")]
    public string Nofollow { get; set; }

    [JsonProperty("custom_attributes")]
    public string CustomAttributes { get; set; }
}