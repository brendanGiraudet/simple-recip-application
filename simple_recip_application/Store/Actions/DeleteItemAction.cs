namespace simple_recip_application.Store.Actions;

public record DeleteItemAction<T>(T Item);
public record DeleteItemSuccessAction<T>(T Item);
public record DeleteItemFailureAction<T>(T Item);
