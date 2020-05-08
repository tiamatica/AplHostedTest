:NameSpace AplBaseClasses
:Using System
:Using DotNetClasses,.\DotNetClasses.dll

    :Class AplBase

        ∇ Make;expr
          :Implements Constructor
          :Access Public
          :Signature AplBase
          ⎕TRAP←(1001 'N')(90 'N')(0 'E' 'getSysErrorException ⎕SIGNAL 90')
        ∇

        ∇ ex←getSysErrorException;ex;errtxt;en
          :Access Public
          errtxt←⎕DMX.Message
          en←⎕DMX.EN
          :Select en
          :Case 3
              ex←⎕NEW AplIndexErrorException(⊂errtxt)
          :Case 5
              ex←⎕NEW AplLengthErrorException(⊂errtxt)
          :Case 11
              ex←⎕NEW AplDomainErrorException(⊂errtxt)
          :Else
              ex←⎕NEW AplSystemErrorException(errtxt en)
          :EndSelect
        ∇

        ∇ ex←getExitSpecException;ex;errtxt
          :Access Public
          errtxt←⎕DMX.Message
          ex←GetException errtxt
        ∇

        ∇ throwExitSpecException txt;ex
          :Access Public
          :Signature throwExitSpecException String txt
          getExitSpecException ⎕SIGNAL 90
        ∇

        ∇ res←Upper txt;st
          :Access Public Overridable
          :Signature String←Upper String txt
          st←⎕NEW String(⊂txt)
          res←st.ToUpper ⍬
        ∇

        ∇ res←GetException txt;es
          :Access Public
          :Signature AplExitSpecificationException←GetException String txt
          es←⎕NEW ExitSpecification(⊂MakeXs txt)
          res←es.AsException
        ∇

        ∇ res←MakeXs txt;_ts
          :Access Private
          _ts←{(100⊥3↑⍵)+((24 60 60⊥⍵[4 5 6])÷86400)}
          res←¯1 ''txt''(0⍴⊂'')0(_ts ⎕TS)'' 0 0
        ∇

    :EndClass
:EndNamespace
