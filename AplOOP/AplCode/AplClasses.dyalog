:NameSpace AplClasses
:Using System
:Using DotNetClasses,.\DotNetClasses.dll
:Using AplBaseClasses,.\AplBase.dll
:Using AplBaseClasses1,.\AplBase1.dll

⍝     :Class Calculator : AplBase1
      :Class Calculator : AplBase
        ∇ Make
          :Access Public Instance
          :Implements Constructor :Base
          :Signature Calculator
        ∇

        ∇ res←Sum args;ex
          :Access Public
          :Signature Int64←Sum Int32[] args
          :Signature Double←SumDouble Double[] args
          :Signature Decimal←Sum Decimal[] args
          :If 2>⊃⍴args
              ex←GetException(⊂'At least 2 numbers are required in the argument')
              ex ⎕SIGNAL 90
⍝             We would really like to be able to make the below call rather than having to make a ⎕SIGMAL call here!
⍝             throwExitSpecException(⊂'At least 2 numbers are required in the argument')
          :EndIf
          res←+/args
        ∇

        ∇ res←Divide(a b);ex
          :Access Public
          :Signature Int32←Divide Int32 divisor, Int32 dividend
          res←a÷b
        ∇

    :EndClass

    :Class ClassWithDispose : AplBase, IDisposable
        :Field Private Instance vBigVar

        ∇ Make
          :Implements Constructor :Base
          :Access Public
          :Signature CalculatorWithDispose
        ∇

        ∇ res←GetNewInstance number;ex
          :Access Public
          :Signature NewClass←GetNewInstance Int32 number
          res←⎕NEW NewClass number
        ∇

        ∇ res←Sum args;ex
          :Access Public
          :Signature Int64←Sum Int32[] args
          :If 2>⊃⍴args
              ex←⎕NEW Exception(⊂'At least 2 numbers are required in the argument')
              ex ⎕SIGNAL 90
          :EndIf
          res←+/args
        ∇

        ∇ res←Divide(dividend divisor);ex;xs
          :Access Public
          :Signature Int64←Divide Int32 dividend, Int32 divisor
          res←dividend÷divisor
        ∇

        ∇ res←WsAwail
          :Access Public
          :Signature Int32←WsAwail
          res←⌊⎕WA÷1024
        ∇

        ∇ res←MakeBigVar arg;rows;cols;content;ex
          :Access Public
          :Signature MakeBigVar Int32 rows, Int32 cols, String content
          rows cols content←arg
          vBigVar←(rows cols)⍴⊂content
        ∇

        ∇ res←GetBigVar
          :Access Public
          :Signature Object←GetBigVar
          res←vBigVar
        ∇

        ∇ DisposeImpl
          :Implements Method IDisposable.Dispose
          destruct
        ∇

        ∇ destruct;sink
          :Implements Destructor
          ⍝ Cleanup!
        ∇

    :EndClass

    :Class TestSystemErrors : AplBase

        ∇ Make
          :Implements Constructor :Base
          :Access Public
          :Signature TestSystemErrors
        ∇

        ∇ TryCreateAPLCore;ex
          :Access Public
          :Signature TryCreateAPLCore
          :Trap 0
              ⎕NA'dyalog64|MEMCPY U U U'
              MEMCPY 255 255 255
          :Else
              ex←⎕NEW Exception,⊂⊂'WOW!! We just trapped an APL core!'
              ex ⎕SIGNAL 90
          :EndTrap
        ∇

    :EndClass

    :Class NewClass : IDisposable
        :Field Private Instance vNumber

        ∇ Make number
          :Implements Constructor
          :Access Public
          :Signature TestClass Int32 number
          vNumber←number
        ∇

        :Property Number
        :Access Public
            ∇ number←get
              :Signature Int32←Id
              number←vNumber
            ∇
        :EndProperty

        ∇ DisposeImpl
          :Implements Method IDisposable.Dispose
          destruct
        ∇

        ∇ destruct;sink
          :Implements Destructor
          ⍝ Cleanup!
        ∇

    :EndClass

:EndNamespace
