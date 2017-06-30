using System;
using System.Text;
using MichaelBrandonMorris.MvcGrid.Interfaces;
using MichaelBrandonMorris.MvcGrid.Models;

namespace MichaelBrandonMorris.MvcGrid.Templating
{
    /// <summary>
    ///     Class SimpleTemplatingEngine.
    /// </summary>
    /// <seealso
    ///     cref="MichaelBrandonMorris.MvcGrid.Interfaces.IMvcGridTemplatingEngine" />
    /// TODO Edit XML Comment Template for SimpleTemplatingEngine
    public class SimpleTemplatingEngine : IMvcGridTemplatingEngine
    {
        /// <summary>
        ///     Processes the specified template.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="model">The model.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for Process
        public string Process(string template, TemplateModel model)
        {
            return string.IsNullOrWhiteSpace(template)
                ? ""
                : Format(template, model);
        }

        /// <summary>
        ///     Evaluates the parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="model">The model.</param>
        /// <returns>System.Object.</returns>
        /// <exception cref="FormatException">
        ///     Format item missing
        ///     prefix: " + name
        /// </exception>
        /// <exception cref="Exception">
        ///     Cannot access cell '"
        ///     + suffix
        ///     + "' in current row. It does not exist or has not yet
        ///     been evaluated
        ///     or
        ///     Invalid prefix in format string: " + prefix
        /// </exception>
        /// TODO Edit XML Comment Template for EvaluateParameter
        private static object EvaluateParameter(
            string name,
            TemplateModel model)
        {
            object val;

            if (string.Compare(
                    name,
                    "value",
                    StringComparison.OrdinalIgnoreCase)
                == 0)
            {
                val = model.Value;
            }
            else
            {
                var dotPos = name.IndexOf(".", StringComparison.Ordinal);

                if (dotPos == -1)
                {
                    throw new FormatException(
                        "Format item missing prefix: " + name);
                }

                var prefix = name.Substring(0, dotPos).Trim().ToLower();
                var suffix = name.Substring(dotPos + 1);


                switch (prefix)
                {
                    case "model":
                        val = ReflectPropertyValue(model.Item, suffix);
                        break;
                    case "row":
                        if (!model.Row.Cells.ContainsKey(suffix))
                        {
                            throw new Exception(
                                "Cannot access cell '"
                                + suffix
                                + "' in current row. It does not exist or has not yet been evaluated");
                        }

                        val = model.Row.Cells[suffix].HtmlText;
                        break;
                    default:
                        throw new Exception(
                            "Invalid prefix in format string: " + prefix);
                }
            }

            return val;
        }

        /// <summary>
        ///     Formats the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="model">The model.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for Format
        private static string Format(string format, TemplateModel model)
        {
            var currentPos = 0;

            var sbResult = new StringBuilder();
            var sbItem = new StringBuilder();

            var inside = false;
            var len = format.Length;

            while (true)
            {
                var c = format[currentPos];

                switch (c)
                {
                    case '{':
                        if (inside)
                        {
                            FormatError();
                        }
                        else if (currentPos < len - 1
                                 && format[currentPos + 1] == '{') //escape char
                        {
                            sbResult.Append('{');
                            currentPos++;
                        }
                        else
                        {
                            inside = true;
                        }
                        break;
                    case '}':
                        if (currentPos < len - 1
                            && format[currentPos + 1] == '}')
                        {
                            sbResult.Append('}');
                            currentPos++;
                        }
                        else
                        {
                            if (!inside)
                            {
                                FormatError();
                            }
                            inside = false;
                            var name = sbItem.ToString();
                            Console.WriteLine(name);

                            sbItem.Clear();

                            sbResult.Append(EvaluateParameter(name, model));
                        }
                        break;
                    default:
                        if (inside)
                        {
                            sbItem.Append(c);
                        }
                        else
                        {
                            sbResult.Append(c);
                        }
                        break;
                }

                currentPos++;
                if (currentPos == format.Length)
                {
                    break;
                }
            }

            if (inside)
            {
                FormatError();
            }

            return sbResult.ToString();
        }

        /// <summary>
        ///     Formats the error.
        /// </summary>
        /// <exception cref="FormatException">
        ///     Input string was not in a
        ///     correct format.
        /// </exception>
        /// TODO Edit XML Comment Template for FormatError
        private static void FormatError()
        {
            throw new FormatException(
                "Input string was not in a correct format.");
        }

        /// <summary>
        ///     Reflects the property value.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        /// <returns>System.Object.</returns>
        /// <exception cref="Exception"></exception>
        /// TODO Edit XML Comment Template for ReflectPropertyValue
        private static object ReflectPropertyValue(
            object source,
            string property)
        {
            var propValue = source;
            foreach (var propName in property.Split('.'))
            {
                var propInfo = propValue.GetType().GetProperty(propName);

                if (propInfo == null)
                {
                    throw new Exception(
                        $"Property {propName} not found on object {source.GetType()}");
                }

                propValue = propInfo.GetValue(propValue, null);

                if (propValue != null)
                {
                    continue;
                }

                break;
            }

            return propValue;
        }
    }
}