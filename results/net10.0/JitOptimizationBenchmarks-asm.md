## .NET 10.0.14 (10.0.14, 10.0.1426.52326), X64 RyuJIT x86-64-v3 (Job: DefaultJob)

```assembly
; JitOptimizationBenchmarks.DirectReturn()
;         return _value;
;         ^^^^^^^^^^^^^^
       mov       eax,[rcx+8]
       ret
; Total bytes of code 4
```

## .NET 10.0.14 (10.0.14, 10.0.1426.52326), X64 RyuJIT x86-64-v3 (Job: DefaultJob)

```assembly
; JitOptimizationBenchmarks.BoxAndUnbox()
;         object boxed = _value;
;         ^^^^^^^^^^^^^^^^^^^^^^
       mov       eax,[rcx+8]
       ret
; Total bytes of code 4
```

## .NET 10.0.14 (10.0.14, 10.0.1426.52326), X64 RyuJIT x86-64-v3 (Job: DefaultJob)

```assembly
; JitOptimizationBenchmarks.EscapingBox()
;         return _value;
;         ^^^^^^^^^^^^^^
       push      rbx
       sub       rsp,20
       mov       rbx,rcx
       mov       rcx,offset MT_System.Int32
       call      CORINFO_HELP_NEWSFAST
       mov       ecx,[rbx+8]
       mov       [rax+8],ecx
       add       rsp,20
       pop       rbx
       ret
; Total bytes of code 35
```

