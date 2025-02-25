namespace simple_recip_application.Store.Actions;

public record AddItemAction<T>(T Item);
public record AddItemSuccessAction<T>(T Item);
public record AddItemFailureAction<T>(T Item);
