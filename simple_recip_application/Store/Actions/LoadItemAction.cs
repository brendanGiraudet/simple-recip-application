namespace simple_recip_application.Store.Actions;

public record LoadItemAction<T>(Guid Id);
public record LoadItemSuccessAction<T>(T Item);
public record LoadItemFailureAction<T>();
