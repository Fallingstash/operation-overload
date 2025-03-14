using System;
using System.Text;
using static SquareMatrix;

class SquareMatrix {
  private int[,] matrix;
  public int Size { get; set; }

  public SquareMatrix(int[,] matrix) {
    this.matrix = matrix;
  }

  public SquareMatrix(int size) {
    Size = size;
    matrix = new int[size, size];
    for (int countOfLines = 0; countOfLines < size; ++countOfLines)
    {
      for (int countOfColumns = 0; countOfColumns < size; ++countOfColumns)
      {
        matrix[countOfLines, countOfColumns] = Convert.ToInt32(Console.ReadLine());
      }
    }
  }
    
  public SquareMatrix(int size, Random random) {
    Size = size;
    matrix = new int[size, size];
    for (int countOfLines = 0; countOfLines < size; ++countOfLines)
    {
      for (int countOfColumns = 0; countOfColumns < size; ++countOfColumns)
      {
        matrix[countOfLines, countOfColumns] = random.Next(0, 9);
      }
    }
  }

  public static SquareMatrix operator +(SquareMatrix a, SquareMatrix b) {
    if (a.Size != b.Size)
    {
      throw new MatrixSizeException("Матрицы должны быть одного размера!");
    }

    SquareMatrix result = new SquareMatrix(a.Size);
    for (int countOfLines = 0; countOfLines < a.Size; ++countOfLines)
    {
      for (int countOfColumns = 0; countOfColumns < a.Size; ++countOfColumns)
      {
        result.matrix[countOfLines, countOfColumns] = a.matrix[countOfLines, countOfColumns] + b.matrix[countOfLines, countOfColumns];
      }
    }
    return result;
  }

  public static SquareMatrix operator *(SquareMatrix a, SquareMatrix b) {
    if (a.Size != b.Size)
    {
      throw new MatrixSizeException("Количество столбцов первой матрицы должно быть равно количеству строк второй!");
    }
    SquareMatrix result = new SquareMatrix(a.Size); // реализуем умножение матриц... 
    for (int countOfLines = 0; countOfLines < a.Size; ++countOfLines)
    {
      for (int countOfColumns = 0; countOfColumns < a.Size; ++countOfColumns)
      {
        for (int countOfRepeats = 0; countOfRepeats < a.Size; ++countOfRepeats)
        {
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

  public int Determinant() { // метод для нахождения детерминанта
    if (Size == 1)
      return matrix[0, 0]; // Определитель матрицы 1x1

    if (Size == 2)
      return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0]; // Определитель матрицы 2x2

    int det = 0;
    for (int countOfColumn = 0; countOfColumn < Size; ++countOfColumn)
    {
      int[,] minor = GetMinor(0, countOfColumn);
      SquareMatrix minorMatrix = new SquareMatrix(minor);

      int minorDet = minorMatrix.Determinant();

      int sign = (countOfColumn % 2 == 0) ? 1 : -1;

      det += sign * matrix[0, countOfColumn] * minorDet;
    }
    return det;
  }

  private int[,] GetMinor(int row, int col) { //реализуем вспомогательный метод дял нахождения детерминанта
    int[,] minor = new int[Size - 1, Size - 1];
    int minorRow = 0, minorCol = 0;

    for (int countOfLine = 0; countOfLine < Size; ++countOfLine)
    {
      if (countOfLine == row)
      {
        continue; // Пропускаем строку row
      }

      for (int countOfColumn = 0; countOfColumn < Size; ++countOfLine)
      {
        if (countOfColumn == col)
        {
          continue; // Пропускаем столбец col
        }

          minor[minorRow, minorCol] = matrix[countOfLine, countOfColumn];
        ++minorCol;
      }
      ++minorRow;
      minorCol = 0;
    }
    return minor;
  }

  public static explicit operator int(SquareMatrix a) { //приведение типа SquareMatrix к типу int (детерминант)
    return a.Determinant();
  }

  public override string ToString() {
    StringBuilder sb = new StringBuilder();
    for (int countOfLines = 0; countOfLines < Size; ++countOfLines)
    {
      for (int countOfColumns = 0; countOfColumns < Size; ++countOfColumns)
      {
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
    for (int countOfLines = 0; countOfLines < Size; ++countOfLines)
    {
      for (int countOfColumns = 0; countOfColumns < Size; ++countOfColumns)
      {
        sum += matrix[countOfLines, countOfColumns];
      }
    }
    return sum;
  }

  public override bool Equals(object obj) {
    if (obj is SquareMatrix other)
    {
      if (this.Size != other.Size)
        return false;
      for (int countOfLines = 0; countOfLines < Size; ++countOfLines)
      {
        for (int countOfColumns = 0; countOfColumns < Size; ++countOfColumns)
        {
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

  public SquareMatrix Clone() {
    SquareMatrix clone = new SquareMatrix(this.Size);
    for (int countOfLines = 0; countOfLines < Size; ++countOfLines)
    {
      for (int countOfColumns = 0; countOfColumns < Size; ++countOfColumns)
      {
        clone.matrix[countOfLines, countOfColumns] = this.matrix[countOfLines, countOfColumns];
      }
    }
    return clone;
  }

  public class MatrixSizeException : Exception {
    public MatrixSizeException() : base("Размеры матриц не совпадают.") { }

    public MatrixSizeException(string message) : base(message) { }

    public MatrixSizeException(string message, Exception innerException) : base(message, innerException) { }
  }
}

class Program {
  static void Main(string[] args) {
    try
    {
      Console.WriteLine("Добро пожаловать в калькулятор квадратных матриц!!! Пожалуйста, выберите операцию:");
      Console.WriteLine("1. Операции с двумя матрицами");
      Console.WriteLine("2. Операции с одной матрицей");

      Random random = new Random();
      string userAnswer = Console.ReadLine();
      string userChoice;
      SquareMatrix matrixA;

      switch (userAnswer)
      {
        case "1":
          Console.Write("Введите размер первой матрицы:");
          int sizeMatrixA = Convert.ToInt32(Console.ReadLine());

          Console.Write("Введите размер второй матрицы:");
          int sizeMatrixB = Convert.ToInt32(Console.ReadLine());

          matrixA = new SquareMatrix(sizeMatrixA, random);
          SquareMatrix matrixB = new SquareMatrix(sizeMatrixB, random);

          Console.WriteLine("Матрица А");
          Console.WriteLine(matrixA);

          Console.WriteLine("Матрица Б");
          Console.WriteLine(matrixB);

          Console.WriteLine("1. Сложить две матрицы.");
          Console.WriteLine("2. Умножить матрицу А на Б");
          Console.WriteLine("3. Сравнить две матрицы");

          userChoice = Console.ReadLine();
          if (userChoice == "1")
          {
            Console.WriteLine("Вы выбрали сложение двух матриц");
            Console.WriteLine(matrixA + matrixB);
          }
          else if (userChoice == "2")
          {
            Console.WriteLine("Вы выбрали умножение матрицы А на Б");
            Console.WriteLine(matrixA * matrixB);
          }
          else if (userChoice == "3")
          {
            Console.WriteLine("Вы выбрали сравнение матриц");
            Console.WriteLine(matrixA == matrixB ? "А = Б" : "А != Б");
            Console.WriteLine(matrixA > matrixB ? "А > Б" : "А <= Б");
          }
          else
          {
            Console.WriteLine("Неверный выбор");
            break;
          }
          break;
        case "2":
          Console.Write("Введите размер матрицы:");
          int sizeMatrix = Convert.ToInt32(Console.ReadLine());
          matrixA = new SquareMatrix(sizeMatrix, random);

          Console.WriteLine("1. Выполнить проверку матрицы(нулевая/не нулевая).");
          Console.WriteLine("2. Создать клон матрицы.");
          Console.WriteLine("3. Найти определитель матрицы.");

          userChoice = Console.ReadLine();
          if (userChoice == "1")
          {
            Console.WriteLine("Вы выбрали проверку матрицы");
            Console.WriteLine(matrixA ? "Это ненулевая матрица" : "Это нулевая матрица");
          }
          else if (userChoice == "2")
          {
            Console.WriteLine("Вы выбрали создать клон матрицы");
            Console.WriteLine("Клон вашей матрицы:");
            SquareMatrix clone = matrixA.Clone();
            Console.WriteLine(clone);
          }
          else if (userChoice == "3")
          {
            Console.WriteLine("Вы выбрали найти определитель матрицы");
            Console.WriteLine(matrixA.Determinant());
          }
          break;
        default:
          Console.WriteLine("Неверный выбор, попробуйте заново");
          break;
      }
    }
    catch (MatrixSizeException ex)
    {
      Console.WriteLine(ex.Message);
    }
  }
}