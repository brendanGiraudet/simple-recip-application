using Fluxor;
using simple_recip_application.Features.Importation.Enums;
using simple_recip_application.Features.Importation.Store.Actions;
using simple_recip_application.Features.RecipesManagement.Store;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.Importation.Store;

public static class ImportReducer
{
    [ReducerMethod]
    public static ImportState ReduceStartImport(ImportState state, StartImportAction action) => state with { IsLoading = true };

    [ReducerMethod]
    public static ImportState ReduceImportSuccess(ImportState state, ImportSuccessAction action) => state with { IsLoading = false, FileContent = null };

    [ReducerMethod]
    public static ImportState ReduceImportFailure(ImportState state, ImportFailureAction action) => state with { IsLoading = false };

    #region SetLoadingAction<ImportStrategyEnum>
    [ReducerMethod]
    public static ImportState ReduceSetLoadingAction(ImportState state, SetLoadingAction<ImportStrategyEnum> action) => state with { IsLoading = action.IsLoading };
    #endregion
}
