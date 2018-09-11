module Suave.Swagger.ArrayTests

open Suave.Swagger
open NUnit.Framework
open System
open Suave.Swagger.Rest
open Suave.Swagger.FunnyDsl
open Suave.Swagger.Swagger
open Suave.Swagger.Serialization

[<CLIMutable>]
type Example1 =
    { items : string array }

[<Test>]
let ``Array of primitives example``() =
    let snapshots = MODEL()
    
    let api =
        swagger { 
            for route in getting (simpleUrl "/foo" |> thenReturns snapshots) do
                yield route |> addResponse 200 "Foo" (Some typeof<Example1>)
                yield route |> supportsJsonAndXml
        }
    
    let x = api.Documentation.ToJson()
    Assert.AreEqual("""{"swagger":"2.0","info":{"title":"","description":"","termsOfService":"","version":""},"schemes":["http"],"paths":{"/foo":{"get":{"summary":"","description":"","operationId":"","consumes":["application/xml","application/json"],"produces":["application/xml","application/json"],"tags":[],"responses":{"200":{"description":"Foo","schema":{"$ref":"#/definitions/Example1"}}}}}},"definitions":{"Example1":{"type":"object","propertiesf":{"items":{
  "type": "array",
  "items": {
    "type": "string",
    "format": "string"
  }
}}}}}""", x)

[<CLIMutable>]
type Example2Child =
    { name : string
      age : int }

[<CLIMutable>]
type Example2 =
    { items : Example2Child array }

[<Test>]
let ``Array of objects example``() =
    let snapshots = MODEL()
    
    let api =
        swagger { 
            for route in getting (simpleUrl "/foo" |> thenReturns snapshots) do
                yield route |> addResponse 200 "Foo" (Some typeof<Example2>)
                yield route |> supportsJsonAndXml
        }
    
    let x = api.Documentation.ToJson()
    Assert.AreEqual("""{"swagger":"2.0","info":{"title":"","description":"","termsOfService":"","version":""},"schemes":["http"],"paths":{"/foo":{"get":{"summary":"","description":"","operationId":"","consumes":["application/xml","application/json"],"produces":["application/xml","application/json"],"tags":[],"responses":{"200":{"description":"Foo","schema":{"$ref":"#/definitions/Example2"}}}}}},"definitions":{"Example2":{"type":"object","propertiesf":{"items":{
  "type": "array",
  "items": {
    "$ref": "#/definitions/Example2Child"
  }
}}},"Example2Child":{"type":"object","propertiesf":{"age":{
  "type": "integer",
  "format": "int32"
},"name":{
  "type": "string",
  "format": "string"
}}}}}""", x)
