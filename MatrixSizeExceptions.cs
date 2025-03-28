using System;

public class MatrixSizeException : Exception {
  public MatrixSizeException() : base("Размеры матриц не совпадают.") { }

  public MatrixSizeException(string message) : base(message) { }

  public MatrixSizeException(string message, Exception innerException)
      : base(message, innerException) { }
}