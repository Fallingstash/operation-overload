using System.Text;
using System;

class SquareMatrix {
  private int[,] matrix;
  public int Size { get; set; }

  public SquareMatrix(int[,] matrix) {
    this.matrix = matrix;
  }

  public SquareMatrix(int size) {
    Size = size;
    matrix = new int[size, size];
    for (int countOfLines = 0; countOfLines < size; ++countOfLines) {
      for (int countOfColumns = 0; countOfColumns < size; ++countOfColumns) {
        matrix[countOfLines, countOfColumns] = Convert.ToInt32(Console.ReadLine());
      }
    }
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
      throw new MatrixSizeException("Матрицы должны быть одного размера!");
    }

    SquareMatrix result = new SquareMatrix(a.Size);
    for (int countOfLines = 0; countOfLines < a.Size; ++countOfLines) {
      for (int countOfColumns = 0; countOfColumns < a.Size; ++countOfColumns) {
        result.matrix[countOfLines, countOfColumns] = a.matrix[countOfLines, countOfColumns] + b.matrix[countOfLines, countOfColumns];
      }
    }
    return result;
  }

  public static SquareMatrix operator *(SquareMatrix a, SquareMatrix b) {
    if (a.Size != b.Size) {
      throw new MatrixSizeException("Количество стобцов первой матрицы, должно быть равно количеству строк второй!");
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

  public static bool operator >(SquareMatrix a, SquareMatrix b) {
    return a.GetSumOfElements() > b.GetSumOfElements();
  }

  public static bool operator <(SquareMatrix a, SquareMatrix b) {
    return a.GetSumOfElements() < b.GetSumOfElements();
  }

  public static bool operator >=(SquareMatrix a, SquareMatrix b) {
    return a.GetSumOfElements() >= b.GetSumOfElements();
  }

  public static bool operator <=(SquareMatrix a, SquareMatrix b) {
    return a.GetSumOfElements() <= b.GetSumOfElements();
  }

  public static bool operator ==(SquareMatrix a, SquareMatrix b) {
    return a.Equals(b);
  }

  public static bool operator !=(SquareMatrix a, SquareMatrix b) {
    return !a.Equals(b);
  }

  public static bool operator true(SquareMatrix a) {
    return a.GetSumOfElements() != 0;
  }

  public static bool operator false(SquareMatrix a) {
    return a.GetSumOfElements() == 0;
  }
  public int Determinant() {
    if (Size == 1)
      return matrix[0, 0]; // Определитель матрицы 1x1

    if (Size == 2)
      return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0]; // Определитель матрицы 2x2

    int det = 0;
    for (int col = 0; col < Size; col++) {
      int[,] minor = GetMinor(0, col);
      SquareMatrix minorMatrix = new SquareMatrix(minor);

      int minorDet = minorMatrix.Determinant();

      int sign = (col % 2 == 0) ? 1 : -1;

      det += sign * matrix[0, col] * minorDet;
    }
    return det;
  }

  private int[,] GetMinor(int row, int col) {
    int[,] minor = new int[Size - 1, Size - 1];
    int minorRow = 0, minorCol = 0;

    for (int i = 0; i < Size; i++) {
      if (i == row) continue; // Пропускаем строку row

      for (int j = 0; j < Size; j++) {
        if (j == col) continue; // Пропускаем столбец col

        minor[minorRow, minorCol] = matrix[i, j];
        minorCol++;
      }
      minorRow++;
      minorCol = 0;
    }
    return minor;
  }

  public static explicit operator int(SquareMatrix a) { //приведение типа SquareMatrix к типу int (детерминант)
    return a.Determinant();
  }

  public override string ToString() {
    StringBuilder sb = new StringBuilder();
    for (int countOfLines = 0; countOfLines < Size; ++countOfLines) {
      for (int countOfColumns = 0; countOfColumns < Size; ++countOfColumns) {
        sb.Append(matrix[countOfLines, countOfColumns] + "\t");
      }
      sb.AppendLine();
    }
    return sb.ToString();
  }

  public int CompareTo(SquareMatrix other) { // (по сумме элементов) положительное - матрица а больше б, отрицательное - б больше а, 0 - матрицы равны
    if (other == null)
      return 1;

    int thisSum = this.GetSumOfElements();
    int otherSum = other.GetSumOfElements();

    return thisSum.CompareTo(otherSum);
  }

  private int GetSumOfElements() {
    int sum = 0;
    for (int countOfLines = 0; countOfLines < Size; ++countOfLines) {
      for (int countOfColumns = 0; countOfColumns < Size; ++countOfColumns) {
        sum += matrix[countOfLines, countOfColumns];
      }
    }
    return sum;
  }

  public override bool Equals(object obj) {
    if (obj is SquareMatrix other) {
      if (this.Size != other.Size)
        return false;
      for (int countOfLines = 0; countOfLines < Size; ++countOfLines) {
        for (int countOfColumns = 0; countOfColumns < Size; ++countOfColumns) {
          if (this.matrix[countOfLines, countOfColumns] != other.matrix[countOfLines, countOfColumns])
            return false;
        }
      }
      return true;
    }
    return false;
  }

  public override int GetHashCode() {
    return matrix.GetHashCode();
  }

  public SquareMatrix DeepClone() {
    SquareMatrix clone = new SquareMatrix(this.Size);
    for (int countOfLines = 0; countOfLines < Size; ++countOfLines) {
      for (int countOfColumns = 0; countOfColumns < Size; ++countOfColumns) {
        clone.matrix[countOfLines, countOfColumns] = this.matrix[countOfLines, countOfColumns];
      }
    }
    return clone;
  }
}