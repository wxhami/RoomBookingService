namespace Application.Common.Exceptions;

public class ObjectNotFoundException(string message = "Запрашиваемый объект не найден в системе") : Exception(message);
