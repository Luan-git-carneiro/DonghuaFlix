public record VideoQualityProfile(
    string QualityName,      // 360p, 720p, 1080p
    int Bitrate,             // Ex: 1_500_000 (1.5 Mbps)
    string Codec,            // h264, h265, av1
    string PlaylistPath,     // caminho para playlist.m3u8
    string SegmentTemplate   // Ex: "segment_$Number$.ts"
);