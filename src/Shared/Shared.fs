namespace InstaFlow.Shared


type Counter = { Value : int }

module Route =
  let builder (typeName: string) (methodName: string) =
    sprintf "/api/%s/%s" typeName methodName

/// A type that specifies the communication protocol between client and server
/// to learn more, read the docs at https://zaid-ajaj.github.io/Fable.Remoting/src/basics.html
[<CompiledName("counter_ahahahah")>]
type CounterApi =
    { initialCounter : unit -> Async<Counter> }

