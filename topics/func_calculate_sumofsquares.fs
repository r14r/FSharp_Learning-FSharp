let square x = x * x

// define the sumOfSquares function
let sumOfSquares n =
   [1..n]
   |> List.map square
   |> List.sum

// try it
printfn "%d" (sumOfSquares 100)


(* = In C# ========================================================================================

public static class SumOfSquaresHelper {
   public static int Square(int i) {
      return i * i;
   }

   public static int SumOfSquares(int n) {
      int sum = 0;
      for (int i = 1; i <= n; i++) {
         sum += Square(i);
      }
      return sum;
   }
}

public static class FunctionalSumOfSquaresHelper {
   public static int SumOfSquares(int n) {
      return Enumerable.Range(1, n)
         .Select(i => i * i)
         .Sum();
   }
}

*)