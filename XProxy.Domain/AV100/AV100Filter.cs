﻿using System;
namespace XProxy.Domain;

/// <summary>
/// Card element, working in BL
/// </summary>
public class AV100Filter
{
    public long Id { get; set; }

    public long YearStart { get; set; }
    public long YearEnd { get; set; }

    public long PriceStart { get; set; }
    public long PriceEnd { get; set; }

    public long DistanceStart { get; set; }
    public long DistanceEnd { get; set; }

    public bool Remont { get; set; }
    public long PackCount { get; set; }

    public long[] RegionIds { get; set; } = Array.Empty<long>();
    public long[] SourceIds { get; set; } = Array.Empty<long>();

    public long[] RegionExternalIds { get; set; } = Array.Empty<long>();
    public long[] SourceExternalIds { get; set; } = Array.Empty<long>();

    public List<AV100Region> AllRegions { get; set; }
    public List<AV100Source> AllSources { get; set; }
}