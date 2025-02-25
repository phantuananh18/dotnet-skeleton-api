using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;
using DotnetSkeleton.SharedKernel.Utils.Models.Requests;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace DotnetSkeleton.SharedKernel.Utils;

/// <summary>
/// A class that contains helper methods for the application.
/// </summary>
public static class Helpers
{
    /// <summary>
    /// A method that builds an error message from an exception.
    /// </summary>
    /// <param name="ex">The exception</param>
    /// <returns>A string that contains the exception</returns>
    public static string BuildErrorMessage(Exception ex)
    {
        return $"Exception: {ex.Message} {(ex.InnerException != null ? ex.InnerException.Message : string.Empty)} ";
    }

    /// <summary>
    /// A method that generates a filter string based on the column name, value type, filter operator, and value.
    /// </summary>
    /// <param name="columnName">Filter column</param>
    /// <param name="valueType">Filter type</param>
    /// <param name="filterOperator">Filter operator</param>
    /// <param name="value">Filter value</param>
    /// <returns>A filter string generated</returns>
    public static string FilterStringGeneration(string columnName, string valueType, string filterOperator, string value)
    {
        if (string.IsNullOrEmpty(columnName) || string.IsNullOrWhiteSpace(columnName)
            || string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)
            || string.IsNullOrEmpty(valueType) || string.IsNullOrWhiteSpace(valueType))
        {
            return string.Empty;
        }

        var filter = string.Empty;
        switch (valueType.ToLower())
        {
            case Constant.FilterValueType.DateTime:
                var dateTime1 = DateTime.Parse(value);
                var strDateTime1 = dateTime1.ToString(Constant.DateTimeFormatter.LongDate);
                var strDateTime2 = dateTime1.AddDays(1).ToString(Constant.DateTimeFormatter.LongDate);

                filter = filterOperator.ToLower() switch
                {
                    Constant.FilterOperator.Equal => $" {columnName} BETWEEN '{strDateTime1}' AND '{strDateTime2}' ",
                    Constant.FilterOperator.NotEqual => $" {columnName} NOT BETWEEN '{strDateTime1}' AND '{strDateTime2}' ",
                    Constant.FilterOperator.LessThan => $" {columnName} < '{strDateTime1}' ",
                    Constant.FilterOperator.LessThanOrEqual => $" {columnName} <= '{strDateTime2}' ",
                    Constant.FilterOperator.GreaterThan => $" {columnName} > '{strDateTime1}' ",
                    Constant.FilterOperator.GreaterThanOrEqual => $" {columnName} >= '{strDateTime1}' ",
                    _ => filter
                };

                break;

            case Constant.FilterValueType.Date:
                var strDate = DateTime.Parse(value).ToString(Constant.DateTimeFormatter.ShortDate);
                filter = filterOperator.ToLower() switch
                {
                    Constant.FilterOperator.Equal => $" {columnName} = '{strDate}' ",
                    Constant.FilterOperator.NotEqual => $" {columnName} != '{strDate}' ",
                    Constant.FilterOperator.LessThan => $" {columnName} < '{strDate}' ",
                    Constant.FilterOperator.LessThanOrEqual => $" {columnName} <= '{strDate}' ",
                    Constant.FilterOperator.GreaterThan => $" {columnName} > '{strDate}' ",
                    Constant.FilterOperator.GreaterThanOrEqual => $" {columnName} >= '{strDate}' ",
                    _ => filter
                };
                break;

            case Constant.FilterValueType.Number:
                filter = filterOperator.ToLower() switch
                {
                    Constant.FilterOperator.Equal => $" {columnName} = {value} ",
                    Constant.FilterOperator.NotEqual => $" {columnName} != {value} ",
                    Constant.FilterOperator.LessThan => $" {columnName} < {value} ",
                    Constant.FilterOperator.LessThanOrEqual => $" {columnName} <= {value} ",
                    Constant.FilterOperator.GreaterThan => $" {columnName} > {value} ",
                    Constant.FilterOperator.GreaterThanOrEqual => $" {columnName} >= {value} ",
                    _ => filter
                };
                break;

            case Constant.FilterValueType.Enum:
                filter = filterOperator.ToLower() switch
                {
                    Constant.FilterOperator.In => $" {columnName} IN ('{value}') ",
                    Constant.FilterOperator.NotIn => $" {columnName} NOT IN ('{value}') ",
                    _ => filter
                };
                break;

            case Constant.FilterValueType.String:
                filter = filterOperator.ToLower() switch
                {
                    Constant.FilterOperator.StartWith => $" {columnName} LIKE '{value}%' ",
                    Constant.FilterOperator.EndWith => $" {columnName} LIKE '%{value}' ",
                    Constant.FilterOperator.Contains => $" {columnName} LIKE '%{value}%' ",
                    Constant.FilterOperator.Equal => $" {columnName} = '{value}' ",
                    _ => filter
                };
                break;

            case Constant.FilterValueType.Boolean:
                var convertValue = ConvertStringBoolToInt(value);
                if (convertValue < 0)
                {
                    break;
                }

                filter = filterOperator.ToLower() switch
                {
                    Constant.FilterOperator.Equal => $" {columnName} = {convertValue} ",
                    Constant.FilterOperator.NotEqual => $" {columnName} != {convertValue} ",
                    _ => filter
                };
                break;

            default:
                break;
        }

        return filter;
    }

    /// <summary>
    /// Convert a string boolean value to integer.
    /// </summary>
    /// <param name="str">String value</param>
    /// <returns>An integer value representing the string's boolean equivalent: 0 for false and 1 for true.</returns>
    public static int ConvertStringBoolToInt(string str)
    {
        if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
        {
            return -1;
        }

        string[] test = { Constant.BooleanValue.True, Constant.BooleanValue.False };
        if (!test.Contains(str.ToLower()))
        {
            return -1;
        }

        return Constant.BooleanValue.True.Equals(str.ToLower()) ? 1 : 0;
    }

    /// <summary>
    /// Generates a SQL sort string based on the provided sort criteria and column mapping.
    /// Each sort input consists of a column and an optional direction (ASC or DESC).
    /// The function maps column names using the provided columnMapping object and defaults to descending if no direction is provided or invalid.
    /// </summary>
    /// <typeparam name="T">The type of the column mapping object, typically a class containing column mappings.</typeparam>
    /// <param name="sorts">An array of strings where each entry represents a column and optional sort direction, separated by a comma.</param>
    /// <param name="columnMapping">An object that maps the provided column names to actual database column names.</param>
    /// <returns>A string representing the SQL ORDER BY clause, with the columns and directions concatenated. </returns>
    public static string SortStringGenerator<T>(string[]? sorts, T columnMapping) where T : class
    {
        if (sorts == null || sorts.Length == 0)
        {
            return string.Empty;
        }

        var sortList = sorts.Select(sort =>
        {
            var sortSplitting = sort.Split(",");
            var columnName = GetPropValue<string>(columnMapping, sortSplitting[0]);
            var sortDirection = (sortSplitting.Length > 1 && !string.IsNullOrWhiteSpace(sortSplitting[1]))
                ? sortSplitting[1].ToLower()
                : Constant.SortDirection.Descending;

            if (sortDirection != Constant.SortDirection.Ascending && sortDirection != Constant.SortDirection.Descending)
            {
                sortDirection = Constant.SortDirection.Descending;
            }

            return string.Join(" ", columnName, sortDirection);
        }).Where(sortClause => !string.IsNullOrWhiteSpace(sortClause));

        return string.Join(", ", sortList);
    }

    /// <summary>
    /// Get the value of a property from an object.
    /// </summary>
    /// <typeparam name="T">A generic type T</typeparam>
    /// <param name="obj">Generic object</param>
    /// <param name="propName">Property name</param>
    /// <returns>A value cast to the generic type T</returns>
    public static T? GetPropValue<T>(object obj, string propName)
    {
        // Convert the input property name to a lower case
        var lowerPropName = propName.ToLower();
        var property = obj.GetType().GetProperties().FirstOrDefault(p => p.Name.ToLower() == lowerPropName);
        if (property == null)
        {
            return default;
        }

        return property.GetValue(obj, null) is T typedValue ? typedValue : default;
    }

    /// <summary>
    /// Get the value of a header from the HTTP context.
    /// </summary>
    /// <param name="context">The current context</param>
    /// <param name="headerName">The header name that need to be retrieved</param>
    /// <returns>A string value representing the value of that header</returns>
    /// <exception cref="ArgumentNullException">Thrown when the context is null</exception>
    /// <exception cref="ArgumentException">Thrown when the header name is null or empty</exception>
    public static string? GetHeaderValue(this HttpContext context, string headerName)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (string.IsNullOrEmpty(headerName))
        {
            throw new ArgumentException("Header cannot be null or empty", nameof(headerName));
        }

        if (!context.Request.Headers.TryGetValue(headerName, out var headerValue))
        {
            return null;
        }

        return headerValue.ToString();
    }

    /// <summary>
    /// Generates a random string consisting of letters.
    /// </summary>
    /// <param name="length">The length of the random string to generate.</param>
    /// <returns>A random string consisting of letters.</returns>
    public static string RandomLetters(int length)
    {
        const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        var random = new Random();
        return new string(Enumerable.Repeat(letters, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    /// <summary>
    /// Generates a random string consisting of numbers.
    /// </summary>
    /// <param name="length">The length of the random string to generate.</param>
    /// <returns>A random string consisting of numbers.</returns>
    public static string RandomNumbers(int length)
    {
        const string numbers = "0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(numbers, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    /// <summary>
    /// Shuffle the characters of a string.
    /// </summary>
    /// <param name="str">The string to shuffle.</param>
    public static string Shuffle(string str)
    {
        var array = str.ToCharArray();
        var random = new Random();
        for (var i = array.Length; i > 1; i--)
        {
            var j = random.Next(i);
            (array[j], array[i - 1]) = (array[i - 1], array[j]);
        }

        return new string(array);
    }

    /// <summary>
    /// Automatically generates a password with the specified length.
    /// </summary>
    /// <param name="length">The total number of chars of password</param>
    /// <returns>A string that represent a generated password (not hashed yet)</returns>
    public static string GeneratePassword(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789^$*[]{}()?!@#%&/><:;|_~=+-";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    /// <summary>
    /// Converts the properties of an object to a dictionary with property names as keys and their values as strings.
    /// </summary>
    /// <typeparam name="TValue">The type of the object to convert.</typeparam>
    /// <param name="obj">The object to convert to a dictionary.</param>
    /// <returns>A dictionary with property names as keys and their string values; returns null if the object is null.</returns>
    public static Dictionary<string, string>? ConvertObjectToDictionary<TValue>(TValue obj)
    {
        if (obj == null)
        {
            return null;
        }

        return typeof(TValue).GetProperties()
            .Where(prop => prop.GetValue(obj) != null)
            .ToDictionary(prop => prop.Name, prop => prop.GetValue(obj)?.ToString() ?? string.Empty);
    }

    /// <summary>
    /// Escapes wildcard characters in a string to prevent them from being interpreted as special characters.
    /// </summary>
    /// <param name="value">The input string to process.</param>
    /// <returns>The string with wildcard characters escaped.</returns>
    public static string ReplaceWildcardCharacters(this string value)
    {
        return string.IsNullOrEmpty(value) 
            ? value 
            : Regex.Replace(value, @"[%_\[\]^()-]", m => @"\" + m.Value);
    }
}