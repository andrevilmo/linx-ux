using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using DataResource = Linx.DS.Core.Data.Resource;


namespace Linx.DS.Core.Data
{
    internal static class ValidationUtilities
    {
        /// <summary>
        /// Creates a new <see cref="ValidationContext"/> for the current object instance.
        /// </summary>
        /// <param name="instance">The object instance being validated.</param>
        /// <param name="parentContext">Optional context to inherit from.  May be null.</param>
        /// <returns>A new validation context.</returns>
        internal static ValidationContext CreateValidationContext(object instance, ValidationContext parentContext)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            ValidationContext context = new ValidationContext(instance, parentContext, parentContext != null ? parentContext.Items : null);
            return context;
        }

        /// <summary>
        /// Internal helper method for getting a method from an object instance that matches
        /// the specified parameters.
        /// </summary>
        /// <param name="instance">Object instance on which the method will be called</param>
        /// <param name="methodName">The name of the method to be called</param>
        /// <param name="parameters">The parameter values to be passed to the method</param>
        /// <returns>A <see cref="MethodInfo"/> from an object instance that matches
        /// the specified parameters.</returns>
        internal static MethodInfo GetMethod(object instance, string methodName, object[] parameters)
        {
            Type instanceType = instance.GetType();
            MethodInfo[] candidates = instanceType.GetTypeInfo().GetMethods()
                .Where(m => m.Name == methodName && IsBindable(m, parameters))
                .ToArray();

            if (candidates.Length == 0)
            {
                int parameterLength = (parameters == null) ? 0 : parameters.Length;
                if (parameterLength == 0)
                {
                    throw new MissingMethodException(string.Format(CultureInfo.CurrentCulture, DataResource.ValidationUtilities_MethodNotFound_ZeroParams, instanceType, methodName));
                }
                else
                {
                    // convert parameter types into a string of this format e.g. ('string', null, 'int')
                    string[] parameterTypes = parameters.Select(p => ((p == null) ? "null" : string.Format(CultureInfo.InvariantCulture, "'{0}'", p.GetType().ToString()))).ToArray();
                    throw new MissingMethodException(string.Format(CultureInfo.CurrentCulture, DataResource.ValidationUtilities_MethodNotFound, instanceType, methodName, parameterLength, string.Join(", ", parameterTypes)));
                }
            }

            if (candidates.Length > 1)
            {
                throw new AmbiguousMatchException(string.Format(CultureInfo.CurrentCulture, DataResource.ValidationUtilities_AmbiguousMatch, methodName));
            }
            return candidates[0];
        }

        internal static bool IsBindable(Type[] parameterTypes, object[] parameters)
        {
            int parameterLength = (parameters == null) ? 0 : parameters.Length;
            if (parameterTypes.Length != parameterLength)
            {
                return false;
            }

            for (int i = 0; i < parameterLength; i++)
            {
                if (parameters[i] == null)
                {
                    if (!TypeUtility.IsNullableType(parameterTypes[i]) && parameterTypes[i].GetTypeInfo().IsValueType)
                    {
                        return false;
                    }
                }
                else if (!parameterTypes[i].GetTypeInfo().IsAssignableFrom(parameters[i].GetType()))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks whether the specified set of parameters can be passed to the specified method.
        /// </summary>
        /// <param name="method">The method to validate the set of parameters against.</param>
        /// <param name="parameters">The set of parameters to check.</param>
        /// <returns><c>true</c> if the set of parameters can be passed to the specified method.</returns>
        internal static bool IsBindable(MethodInfo method, object[] parameters)
        {
            return IsBindable(method.GetParameters().Select(p => p.ParameterType).ToArray(), parameters);
        }

        /// <summary>
        /// Appends the specified memberPath to all member names in the validation results.
        /// </summary>
        /// <param name="validationResults">The validation results</param>
        /// <param name="memberPath">The member path to append</param>
        /// <returns>The updated validation results</returns>
        internal static IEnumerable<ValidationResult> ApplyMemberPath(IEnumerable<ValidationResult> validationResults, string memberPath)
        {
            if (string.IsNullOrEmpty(memberPath))
            {
                // no path to apply
                return validationResults;
            }

            return validationResults.Select(p => ApplyMemberPath(p, memberPath));
        }

        /// <summary>
        /// Appends the specified memberPath to all member names in the validation result.
        /// </summary>
        /// <param name="validationResult">The validation result</param>
        /// <param name="memberPath">The member path to append</param>
        /// <returns>The updated validation result</returns>
        internal static ValidationResult ApplyMemberPath(ValidationResult validationResult, string memberPath)
        {
            if (string.IsNullOrEmpty(memberPath))
            {
                // no path to apply
                return validationResult;
            }

            List<string> memberNames = new List<string>();
            foreach (string currMemberName in validationResult.MemberNames)
            {
                string transformedMemberName = memberPath + "." + currMemberName;
                memberNames.Add(transformedMemberName);
            }

            if (memberNames.Count == 0)
            {
                // If this is a type level validation error, there won't be any member names
                // so we need to add the member path. We use a terminating '.' to differentiate
                // between Type level and Property level errors. Otherwise, for an error with a
                // member name like "ContactInfo.Address" we wouldnt be able to determine if
                // the error applies to the Contact.Address property or the Contact.Address instance.
                memberNames.Add(memberPath + ".");
            }

            return new ValidationResult(validationResult.ErrorMessage, memberNames);
        }

        /// <summary>
        /// Validate the specified object an any complex members or collections recursively.
        /// </summary>
        /// <param name="instance">The instance to validate.</param>
        /// <param name="validationContext">The validation context</param>
        /// <param name="validationResults">The validation results</param>
        /// <returns>True if the object is valid, false otherwise.</returns>
        public static bool TryValidateObject(object instance, ValidationContext validationContext, List<ValidationResult> validationResults)
        {
            return ValidateObjectRecursive(instance, string.Empty, validationContext, validationResults);
        }

        /// <summary>
        /// This method recursively validates an object, first validating all properties, then
        /// validating the type. This method implements the classic Try pattern. However it serves
        /// as code sharing for the validation pattern where an exception is thrown on first error.
        /// </summary>
        /// <param name="instance">The object to validate.</param>
        /// <param name="memberPath">The dotted path of the member.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <param name="validationResults">The collection in which the validation results will be
        /// stored. The collection can be <c>null</c>.</param>
        /// <returns><c>True</c> if the object was successfully validated with no errors.</returns>
        /// <exception cref="ValidationException">When <paramref name="validationResults"/> is
        /// <c>null</c> and the object has a validation error.</exception>
        private static bool ValidateObjectRecursive(object instance, string memberPath,
            ValidationContext validationContext, List<ValidationResult> validationResults)
        {
            MetaType metaType = MetaType.GetMetaType(instance.GetType());
            if (!metaType.RequiresValidation)
            {
                return true;
            }

            // First validate all properties
            bool hasValidationErrors = false;
            foreach (MetaMember metaMember in metaType.Members.Where(m => m.RequiresValidation || m.IsComplex))
            {
                ValidationContext propertyValidationContext = ValidationUtilities.CreateValidationContext(instance, validationContext);
                propertyValidationContext.MemberName = metaMember.Member.Name;

                // Form the current member path, appending the current
                // member name if it is complex.
                string currMemberPath = memberPath;
                if (metaMember.IsComplex)
                {
                    if (currMemberPath.Length > 0)
                    {
                        currMemberPath += ".";
                    }
                    currMemberPath += metaMember.Member.Name;
                }

                object value = metaMember.GetValue(instance);

                // first validate the property itself
                if (metaMember.RequiresValidation)
                {
                    hasValidationErrors |= !ValidationUtilities.ValidateProperty(value, propertyValidationContext, validationResults, currMemberPath);
                }

                // for complex members, in addition to property level validation we need to
                // do deep validation recursively
                if (value != null && metaMember.IsComplex)
                {
                    if (!metaMember.IsCollection)
                    {
                        hasValidationErrors |= !ValidateObjectRecursive(value, currMemberPath, validationContext, validationResults);
                    }
                    else
                    {
                        hasValidationErrors |= !ValidateComplexCollection((IEnumerable)value, currMemberPath, validationContext, validationResults);
                    }
                }
            }

            // Only proceed to Type level validation if there are no property validation errors
            if (hasValidationErrors)
            {
                return false;
            }

            // Next perform Type level validation without validating properties, since we've already validated all properties.
            // Note that we can't use Validator.ValidateObject specifying 'validateAllProperties' since even when specifying false,
            // that API will validate RequiredAttribute.
            ValidationContext context = ValidationUtilities.CreateValidationContext(instance, validationContext);
            if (metaType.ValidationAttributes.Any())
            {
                hasValidationErrors |= !ValidationUtilities.ValidateValue(instance, context, validationResults, metaType.ValidationAttributes, memberPath);
            }

            // Only proceed to IValidatableObject validation if there are no errors
            if (hasValidationErrors)
            {
                return false;
            }

            // Test for IValidatableObject implementation and run the validation if applicable
            // Note : this interface doesn't exist in Silverlight
            IValidatableObject validatable = instance as IValidatableObject;
            if (validatable != null)
            {
                IEnumerable<ValidationResult> results = validatable.Validate(context);

                if (!string.IsNullOrEmpty(memberPath))
                {
                    results = ValidationUtilities.ApplyMemberPath(results, memberPath);
                }

                foreach (ValidationResult result in results.Where(r => r != ValidationResult.Success))
                {
                    validationResults.Add(result);
                    hasValidationErrors = true;
                }
            }


            return !hasValidationErrors;
        }

        /// <summary>
        /// This method deeply validates all objects in a collection. This method implements the
        /// classic Try pattern. However it serves as code sharing for the validation pattern where
        /// an exception is thrown on first error.
        /// </summary>
        /// <param name="elements">The enumerable containing the objects to validate.</param>
        /// <param name="memberPath">The dotted path of the member.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <param name="validationResults">The collection in which the validation results will be
        /// stored. The collection can be <c>null</c>.</param>
        /// <returns><c>True</c> if the object was successfully validated with no errors.</returns>
        /// <exception cref="ValidationException">When <paramref name="validationResults"/> is
        /// <c>null</c> and the object has a validation error.</exception>
        private static bool ValidateComplexCollection(IEnumerable elements, string memberPath,
            ValidationContext validationContext, List<ValidationResult> validationResults)
        {
            bool hasValidationErrors = false;

            foreach (var element in elements)
            {
                if (element == null)
                {
                    continue;
                }

                hasValidationErrors |= !ValidateObjectRecursive(element, memberPath + "()", validationContext, validationResults);
            }

            return !hasValidationErrors;
        }

        private static bool ValidateProperty(object value, ValidationContext validationContext,
            List<ValidationResult> validationResults, string memberPath)
        {
            if (validationResults == null)
            {
                Validator.ValidateProperty(value, validationContext);
            }
            else
            {
                List<ValidationResult> currentResults = new List<ValidationResult>();
                if (!Validator.TryValidateProperty(value, validationContext, currentResults))
                {
                    // transform the validation results by applying the member path to the results
                    if (memberPath.Length > 0)
                    {
                        currentResults = ValidationUtilities.ApplyMemberPath(currentResults, memberPath).ToList();
                    }
                    validationResults.AddRange(currentResults);
                    return false;
                }
            }

            return true;
        }

        private static bool ValidateValue(object value, ValidationContext validationContext,
            List<ValidationResult> validationResults, IEnumerable<ValidationAttribute> validationAttributes,
            string memberPath)
        {
            if (validationResults == null)
            {
                Validator.ValidateValue(value, validationContext, validationAttributes);
            }
            else
            {
                // todo, needs to be array aware
                List<ValidationResult> currentResults = new List<ValidationResult>();
                if (!Validator.TryValidateValue(value, validationContext, currentResults, validationAttributes))
                {
                    // transform the validation results by applying the member path to the results
                    if (!string.IsNullOrEmpty(memberPath))
                    {
                        currentResults = ValidationUtilities.ApplyMemberPath(currentResults, memberPath).ToList();
                    }
                    validationResults.AddRange(currentResults);
                    return false;
                }
            }
            return true;
        }


        internal static bool TryValidateMethodCall(DomainOperationEntry operationEntry, ValidationContext validationContext, object[] parameters, List<ValidationResult> validationResults)
        {
            bool breakOnFirstError = validationResults == null;

            ValidationContext methodContext = CreateValidationContext(validationContext.ObjectInstance, validationContext);
            methodContext.MemberName = operationEntry.Name;

            DisplayAttribute display = (DisplayAttribute)operationEntry.Attributes[typeof(DisplayAttribute)];

            if (display != null)
            {
                methodContext.DisplayName = display.GetName();
            }

            string methodPath = string.Empty;
            if (operationEntry.Operation == DomainOperation.Custom)
            {
                methodPath = operationEntry.Name + ".";
            }

            IEnumerable<ValidationAttribute> validationAttributes = operationEntry.Attributes.OfType<ValidationAttribute>();
            bool success = Validator.TryValidateValue(validationContext.ObjectInstance, methodContext, validationResults, validationAttributes);

            if (!breakOnFirstError || success)
            {
                for (int paramIndex = 0; paramIndex < operationEntry.Parameters.Count; paramIndex++)
                {
                    DomainOperationParameter methodParameter = operationEntry.Parameters[paramIndex];
                    object value = (parameters.Length > paramIndex ? parameters[paramIndex] : null);

                    ValidationContext parameterContext = ValidationUtilities.CreateValidationContext(validationContext.ObjectInstance, validationContext);
                    parameterContext.MemberName = methodParameter.Name;

                    string paramName = methodParameter.Name;

                    AttributeCollection parameterAttributes = methodParameter.Attributes;
                    display = (DisplayAttribute)parameterAttributes[typeof(DisplayAttribute)];

                    if (display != null)
                    {
                        paramName = display.GetName();
                    }

                    parameterContext.DisplayName = paramName;

                    string parameterPath = string.Empty;
                    if (!string.IsNullOrEmpty(methodPath) && paramIndex > 0)
                    {
                        parameterPath = methodPath + methodParameter.Name;
                    }

                    IEnumerable<ValidationAttribute> parameterValidationAttributes = parameterAttributes.OfType<ValidationAttribute>();
                    bool parameterSuccess = ValidationUtilities.ValidateValue(value, parameterContext, validationResults, parameterValidationAttributes,
                        ValidationUtilities.NormalizeMemberPath(parameterPath, methodParameter.ParameterType));
                    
                    // Custom methods run deep validation as well as parameter validation.
                    // If parameter validation has already failed, stop further validation.
                    if (parameterSuccess && operationEntry.Operation == DomainOperation.Custom && value != null)
                    {
                        Type parameterType = methodParameter.ParameterType;

                        if (TypeUtility.IsComplexType(parameterType))
                        {
                            parameterSuccess = ValidationUtilities.ValidateObjectRecursive(value, parameterPath, parameterContext, validationResults);
                        }
                        else if (TypeUtility.IsComplexTypeCollection(parameterType))
                        {
                            parameterSuccess = ValidationUtilities.ValidateComplexCollection(value as IEnumerable, parameterPath, parameterContext, validationResults);
                        }
                    }

                    success &= parameterSuccess;

                    if (breakOnFirstError && !success)
                    {
                        break;
                    }
                }
            }

            return success;
        }

        /// <summary>
        /// Adds the collection token '()' to the <paramref name="memberPath"/> if the
        /// <paramref name="memberType"/> is an IEnumerable.
        /// </summary>
        /// <param name="memberPath">The path of the member.</param>
        /// <param name="memberType">The type of the member.</param>
        /// <returns>The correct member representation.</returns>
        private static string NormalizeMemberPath(string memberPath, Type memberType)
        {
            if (string.IsNullOrEmpty(memberPath))
            {
                return memberPath;
            }

            if (typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(memberType))
            {
                Debug.Assert(!memberPath.EndsWith("()", StringComparison.Ordinal), "The memberPath already contains a () suffix.");
                return memberPath + "()";
            }

            return memberPath;
        }
    }


    internal class ValidationResultEqualityComparer : EqualityComparer<ValidationResult>
    {
        public override bool Equals(ValidationResult left, ValidationResult right)
        {
            if (left.ErrorMessage.Equals(right.ErrorMessage, StringComparison.Ordinal) && left.MemberNames.SequenceEqual(right.MemberNames))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode(ValidationResult validationResult)
        {
            int hashCode = validationResult.ErrorMessage.GetHashCode();
            foreach (string memberName in validationResult.MemberNames)
            {
                hashCode ^= memberName.GetHashCode();
            }
            return hashCode;
        }
    }

}
