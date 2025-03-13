using System;
using System.Text;
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

  public override string ToString(SquareMatrix a) {
    StringBuilder sb = new StringBuilder();
    for (int countOfLines = 0; countOfLines < a.Size; ++countOfLines) {
      for (int countOfColumns = 0; countOfColumns < a.Size; ++countOfColumns) {
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
    for (int i = 0; i < Size; i++) {
      for (int j = 0; j < Size; j++) {
        sum += matrix[i, j];
      }
    }
    return sum;
  }

  public override bool Equals(object obj) {
    if (obj is SquareMatrix other) {
      if (this.Size != other.Size)
        return false;
      for (int i = 0; i < Size; i++) {
        for (int j = 0; j < Size; j++) {
          if (this.matrix[i, j] != other.matrix[i, j])
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

  public SquareMatrix Clone() {
    SquareMatrix clone = new SquareMatrix(this.Size);
    for (int i = 0; i < Size; i++) {
      for (int j = 0; j < Size; j++) {
        clone.matrix[i, j] = this.matrix[i, j];
      }
    }
    return clone;
  }
}
