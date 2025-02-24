using Fluxor;
using simple_recip_application.Features.Importation.Store.Actions;

namespace simple_recip_application.Features.Importation.Store;

public static class ImportReducer
{
    [ReducerMethod]
    public static ImportState ReduceStartImport(ImportState state, StartImportAction action) => state with { IsLoading = true };

    [ReducerMethod]
    public static ImportState ReduceImportSuccess(ImportState state, ImportSuccessAction action) => state with { IsLoading = false, FileContent = null };

    [ReducerMethod]
    public static ImportState ReduceImportFailure(ImportState state, ImportFailureAction action) => state with { IsLoading = false };


    [ReducerMethod]
    public static ImportState ReduceSetFileContentAction(ImportState state, SetFileContentAction action) => state with { FileContent = action.FileContent };
}
