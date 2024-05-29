﻿/*******************************************************************************
* ABOUT THIS FILE                                                              *
********************************************************************************
*                                                                              *
* This file contains C# code to serialize (write) and deserialize (read) JSON  *
* objects for ProGet HTTP Endpoints. It also serves as the specifications for  *
* the expected format of these JSON objects; this is why it doesn't follow     *
* normal C# commenting contentions/standards.                                  *
*                                                                              *
* If you're not familiar with JSON Serialization in C#, a few notes:           *
*                                                                              *
* - The C# property names are PascalCase, but they are converted to camelCase  *
*   for JSON; e.g. "MyProperty" will become "myProperty"                       *
*                                                                              *
* - Some C# properties will reference enums; these are converted to camelCase  *
*   string values; e.g. "MyValue" will become "myValue"                        *
*                                                                              *
* - Date types are forgiving, but you should specify ISO8601 and C# will       *
*   output something like "2019-08-01T00:00:00-07:00"                          *
*                                                                              *
* - A type with a ? (e.g. int?) means that the JSON property may be missing    *
*   or null. This usually means that it's an optional property, but it also    *
*   could mean that it's required only in some contexts                        *
*                                                                              *
* - The "required" keyword means that the JSON property should always be       *
*   present (when listing) or must be specified when creating/editing          *
*                                                                              *
*******************************************************************************/

namespace Inedo.ProGet;

// JSON Object used by Create ApiKey and List ApiKey HTTP endpoints
public sealed class ApiKeyInfo
{
    // ProGet-generated unique ID for an API key; used when deleting a key and can never be set
    public int? Id { get; set; }

    public required ApiKeyType Type { get; set; }

    // Api Key text itself; 
    // * When creating keys, this will be generated by ProGet when not specified.
    // * When cryptographic hashing is enabled in ProGet, this will always be null and may never be set on create
    public string? Key { get; set; }

    // Used in lists of keys, and when not specified, a name like "(ID=1000)" is displayed
    public string? DisplayName { get; set; }

    // Used to describe the usage or other context about the key
    public string? Description { get; set; }

    // APIs a system key may use
    // * Required when <see cref="Type"/> is <see cref="ApiKeyType.Feed"/>; otherwise must be null
    // * Value is either ["full-control"] or a combination of "feeds", "sca", "sbom-upload"
    public string[]? SystemApis { get; set; }

    // Permissions a feed key may have
    // * Value is a combination of "view","add","promote","delete"
    // * Required when Type is Feed; otherwise must be null
    public string[]? PackagePermissions { get; set; }

    // Name of the feed the feed key applies to
    // * Optional when Type is Feed; otherwise must be null
    // * Must be null when FeedGroup has a value
    public string? Feed { get; set; }

    /// Name of the feed group the key applies to
    // * Optional when Type is Feed; otherwise must be null
    // * Must be null when Feed has a value
    public string? FeedGroup { get; set; }

    public DateTime? Expiration { get; set; }
    public required ApiKeyBodyLogging Logging { get; set; }

    /// Name of the user the personal key applies to; required when Type is Personal (otherwise must be null)
    public string? User { get; set; }
}
