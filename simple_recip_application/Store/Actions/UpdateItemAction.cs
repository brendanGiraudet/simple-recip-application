namespace simple_recip_application.Store.Actions;

public record UpdateItemAction<T>(T Item);
public record UpdateItemSuccessAction<T>(T Item);
public record UpdateItemFailureAction<T>(T Item);
