:NameSpace SimpleAplSample
:Using System

    :Class SimpleAplClass

        ∇ Make
          :Implements Constructor
          :Access Public
          :Signature SimpleAplClass

        ∇

        ∇ res←Call loopsize;i;a 
          :Access Public
          :Signature Int32←Call Int32 loopsize
		  :For i :In ⍳loopsize
             a←100 i⍴⍳i
             a←a×a
          :EndFor
          res←1
        ∇

	    ∇ res←CallWithArgument arg
          :Access Public
          :Signature Int32←CallWithArgument Int32
          res←arg
        ∇
    :EndClass
:EndNamespace
