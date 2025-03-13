using System;
using System.Runtime.InteropServices;

class SquareMatrix {
  private int[,] matrix;
  public int Size { get; set; }

  public SquareMatrix(int size) {
    Size = size;
    matrix = new int[size, size];
  }

  public SquareMatrix(int size, Random random) {
    Size = size;
    matrix = new int[size, size];
    for (int countOfLines = 0; countOfLines < size; ++countOfLines) {
      for (int countOfColumns = 0; countOfColumns < size; ++countOfColumns) {
        matrix[countOfLines, countOfColumns] = random.Next(0, 9);
      }
    }
  }

  public static SquareMatrix operator +(SquareMatrix a, SquareMatrix b) {
    if (a.Size != b.Size) { 
      throw new InvalidOperationException("Матрицы должны быть одного размера!");
    }

    SquareMatrix result = new SquareMatrix(a.Size);
    for (int countOfLines = 0; countOfLines < a.Size; ++countOfLines) {
      for (int countOfColumns = 0; countOfColumns < a.Size; ++countOfColumns) {
        result.matrix[countOfLines, countOfColumns] = a.matrix[countOfLines, countOfColumns] + b.matrix[countOfLines, countOfColumns];
      }
    }
    return result;
  }

  public static bool operator ==(SquareMatrix a, SquareMatrix b) {
    if (a.Size != b.Size) {
      return false;
    }
    bool result = true;
    for (int countOfLines = 0; countOfLines < a.Size; ++countOfLines) {
      for (int countOfColumns = 0; countOfColumns < a.Size; ++countOfColumns) {
        if (a.matrix[countOfLines, countOfColumns] != b.matrix[countOfLines, countOfColumns]) {
          result = false;
          return result;
        }
      }
    }
    return result;
  }
  
  public static bool operator !=(SquareMatrix a, SquareMatrix b) {
    if (a.Size != b.Size) {
      return true;
    }
    bool result = false;
    for (int countOfLines = 0; countOfLines < a.Size; ++countOfLines) {
      for (int countOfColumns = 0; countOfColumns < a.Size; ++countOfColumns) {
        if (a.matrix[countOfLines, countOfColumns] != b.matrix[countOfLines, countOfColumns]) {
          result = true;
          return result;
        }
      }
    }
    return result;
  }

  public static SquareMatrix operator *(SquareMatrix a, SquareMatrix b) {
    if (a.Size != b.Size) {
      throw new InvalidOperationException("Количество стобцов первой матрицы, должно быть равно количеству строк второй!");
    }
    SquareMatrix result = new SquareMatrix(a.Size); // реализуем умножение матриц... (я придумывал это 2 часа)
    for (int countOfLines = 0; countOfLines < a.Size; ++countOfLines) {
      for (int countOfColumns = 0; countOfColumns < a.Size; ++countOfColumns) {
        for (int countOfRepeats = 0; countOfRepeats < a.Size; ++countOfRepeats) {
          result.matrix[countOfLines, countOfColumns] += a.matrix[countOfLines, countOfRepeats] * b.matrix[countOfRepeats, countOfColumns];
        }
      }
    }
    return result;
  }
