using Fluxor;

namespace simple_recip_application.Features.Importation.Store;

[FeatureState]
public record class ImportState
{
    public bool IsLoading { get; set; }
    public byte[]? FileContent { get; set; }
}
