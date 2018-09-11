module Suave.Swagger.ArrayTests

open Suave.Swagger
open NUnit.Framework
open System
open Suave.Swagger.Rest
open Suave.Swagger.FunnyDsl
open Suave.Swagger.Swagger
open Suave.Swagger.Serialization

[<CLIMutable>]
type TimeResult =
    { items : string [] }
    
(*
{
    "swagger": "2.0",
    "info": {
        "title": "",
        "description": "",
        "termsOfService": "",
        "version": ""
    },
    "schemes": ["http"],
    "paths": {
        "/snapshots": {
            "get": {
                "summary": "",
                "description": "",
                "operationId": "",
                "consumes": ["application/xml", "application/json"],
                "produces": ["application/xml", "application/json"],
                "tags": [],
                "responses": {
                    "200": {
                        "description": "Featured snapshots",
                        "schema": {
                            "$ref": "#/definitions/TimeResult"
                        }
                    }
                }
            }
        }
    },
    "definitions": {
        "TimeResult": {
            "type": "object",
            "properties": {
                "items": {
                    "$ref": "#/definitions/String[]"
                }
            }
        },
        "String[]": {
            "type": "object",
            "properties": {
                "IsFixedSize": {
                    "type": "boolean",
                    "format": ""
                },
                "IsReadOnly": {
                    "type": "boolean",
                    "format": ""
                },
                "IsSynchronized": {
                    "type": "boolean",
                    "format": ""
                },
                "Length": {
                    "type": "integer",
                    "format": "int32"
                },
                "LongLength": {
                    "type": "integer",
                    "format": "int64"
                },
                "Rank": {
                    "type": "integer",
                    "format": "int32"
                },
                "SyncRoot": {
                    "$ref": "#/definitions/Object"
                }
            }
        }
    }
}
*)

[<Test>]
let ``todo name``() =
    let snapshots = MODEL { items = [||] }
    
    let api =
        swagger { 
            for route in getting (simpleUrl "/snapshots" |> thenReturns snapshots) do
                yield route |> addResponse 200 "Featured snapshots" (Some typeof<TimeResult>)
                yield route |> supportsJsonAndXml
        }
    let x = api.Documentation.ToJson ()
    Assert.AreEqual("", x)
    ()
