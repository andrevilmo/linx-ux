// -----------------------------------------------------------------------
// <copyright file="NlogExtension.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Linx.Internet.Application
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using NLog;

    /// <summary>
    /// Classe ObjectExtension
    /// </summary>
    public static class NlogExtension
    {
        /// <summary>
        /// instância atual de logger
        /// </summary>
        private static Logger log;

        /// <summary>
        /// Grava a informação de log
        /// </summary>
        /// <param name="referencia">Referência do log</param>
        /// <param name="typeName">Tipo do log</param>
        /// <param name="typeValue">Tipo do valor</param>
        /// <param name="ex">Exception de erro</param>
        /// <param name="message">Mensagem do log</param>
        /// <param name="args">Argumentos da função</param>
        public static void InfoLinx(this Logger referencia, string typeName, string typeValue, Exception ex, string message, params object[] args)
        {
            log = referencia;
            CreateLog(LogLevel.Info, typeName, typeValue, ex, message, args);
        }

        /// <summary>
        /// Grava a informação de log
        /// </summary>
        /// <param name="referencia">Referência do log</param>
        /// <param name="typeName">Tipo do log</param>
        /// <param name="typeValue">Tipo do valor</param>
        /// <param name="message">Mensagem do log</param>
        /// <param name="args">Argumentos da função</param>
        public static void InfoLinx(this Logger referencia, string typeName, string typeValue, string message, params object[] args)
        {
            log = referencia;
            CreateLog(LogLevel.Info, typeName, typeValue, null, message, args);
        }

        /// <summary>
        /// Grava trace do log
        /// </summary>
        /// <param name="referencia">Referência do log</param>
        /// <param name="typeName">Tipo do log</param>
        /// <param name="typeValue">Tipo do valor</param>
        /// <param name="message">Mensagem do log</param>
        /// <param name="args">Argumentos da função</param>
        public static void TraceLinx(this Logger referencia, string typeName, string typeValue, string message, params object[] args)
        {
            log = referencia;
            CreateLog(LogLevel.Trace, typeName, typeValue, null, message, args);
        }

        /// <summary>
        /// Grava trace do log
        /// </summary>
        /// <param name="referencia">Referência do log</param>
        /// <param name="typeName">Tipo do log</param>
        /// <param name="typeValue">Tipo do valor</param>
        /// <param name="ex">Exception lançada</param>
        /// <param name="message">Mensagem do log</param>
        /// <param name="args">Argumentos da função</param>
        public static void TraceLinx(this Logger referencia, string typeName, string typeValue, Exception ex, string message, params object[] args)
        {
            log = referencia;
            CreateLog(LogLevel.Trace, typeName, typeValue, ex, message, args);
        }
        
        /// <summary>
        /// Grava o warning no log
        /// </summary>
        /// <param name="referencia">Referência do log</param>
        /// <param name="typeName">Tipo do log</param>
        /// <param name="typeValue">Tipo do valor</param>
        /// <param name="message">Mensagem do log</param>
        /// <param name="args">Argumentos da função</param>
        public static void WarnLinx(this Logger referencia, string typeName, string typeValue, string message, params object[] args)
        {
            log = referencia;
            CreateLog(LogLevel.Warn, typeName, typeValue, null, message, args);
        }

        /// <summary>
        /// Grava o warning no log
        /// </summary>
        /// <param name="referencia">Referência do log</param>
        /// <param name="typeName">Tipo do log</param>
        /// <param name="typeValue">Tipo do valor</param>
        /// <param name="ex">Exception lançada</param>
        /// <param name="message">Mensagem do log</param>
        /// <param name="args">Argumentos da função</param>
        public static void WarnLinx(this Logger referencia, string typeName, string typeValue, Exception ex, string message, params object[] args)
        {
            log = referencia;
            CreateLog(LogLevel.Warn, typeName, typeValue, ex, message, args);
        }

        /// <summary>
        /// Grava o erro no log
        /// </summary>
        /// <param name="referencia">Referência do log</param>
        /// <param name="typeName">Tipo do log</param>
        /// <param name="typeValue">Tipo do valor</param>
        /// <param name="message">Mensagem do log</param>
        /// <param name="args">Argumentos da função</param>
        public static void ErrorLinx(this Logger referencia, string typeName, string typeValue, string message, params object[] args)
        {
            log = referencia;
            CreateLog(LogLevel.Error, typeName, typeValue, null, message, args);
        }

        /// <summary>
        /// Grava o erro no log
        /// </summary>
        /// <param name="referencia">Referência do log</param>
        /// <param name="typeName">Tipo do log</param>
        /// <param name="typeValue">Tipo do valor</param>
        /// <param name="ex">Exception lançada</param>
        /// <param name="message">Mensagem do log</param>
        /// <param name="args">Argumentos da função</param>
        public static void ErrorLinx(this Logger referencia, string typeName, string typeValue, Exception ex, string message, params object[] args)
        {
            log = referencia;
            CreateLog(LogLevel.Error, typeName, typeValue, ex, message, args);
        }

        /// <summary>
        /// Grava o debug no log
        /// </summary>
        /// <param name="referencia">Referência do log</param>
        /// <param name="typeName">Tipo do log</param>
        /// <param name="typeValue">Tipo do valor</param>
        /// <param name="message">Mensagem do log</param>
        /// <param name="args">Argumentos da função</param>
        public static void DebugLinx(this Logger referencia, string typeName, string typeValue, string message, params object[] args)
        {
            log = referencia;
            CreateLog(LogLevel.Debug, typeName, typeValue, null, message, args);
        }

        /// <summary>
        /// Grava o debug no log
        /// </summary>
        /// <param name="referencia">Referência do log</param>
        /// <param name="typeName">Tipo do log</param>
        /// <param name="typeValue">Tipo do valor</param>
        /// <param name="ex">Esxception lançada</param>
        /// <param name="message">Mensagem do log</param>
        /// <param name="args">Argumentos da função</param>
        public static void DebugLinx(this Logger referencia, string typeName, string typeValue, Exception ex, string message, params object[] args)
        {
            log = referencia;
            CreateLog(LogLevel.Debug, typeName, typeValue, ex, message, args);
        }

        /// <summary>
        /// Grava o fatal no log
        /// </summary>
        /// <param name="referencia">Referência do log</param>
        /// <param name="typeName">Tipo do log</param>
        /// <param name="typeValue">Tipo do valor</param>
        /// <param name="message">Mensagem do log</param>
        /// <param name="args">Argumentos da função</param>
        public static void FatalLinx(this Logger referencia, string typeName, string typeValue, string message, params object[] args)
        {
            log = referencia;
            CreateLog(LogLevel.Fatal, typeName, typeValue, null, message, args);
        }

        /// <summary>
        /// Grava o fatal no log
        /// </summary>
        /// <param name="referencia">Referência do log</param>
        /// <param name="typeName">Tipo do log</param>
        /// <param name="typeValue">Tipo do valor</param>
        /// <param name="ex">Exception lançada</param>
        /// <param name="message">Mensagem do log</param>
        /// <param name="args">Argumentos da função</param>
        public static void FatalLinx(this Logger referencia, string typeName, string typeValue, Exception ex, string message, params object[] args)
        {
            log = referencia;
            CreateLog(LogLevel.Fatal, typeName, typeValue, ex, message, args);
        }

        /// <summary>
        /// Cria o log
        /// </summary>
        /// <param name="level">Nível do log</param>
        /// <param name="typeName">Tipo do log</param>
        /// <param name="typeValue">Tipo do valor</param>
        /// <param name="ex">Exception lançada</param>
        /// <param name="message">Mensagem do log</param>
        /// <param name="args">Argumentos da função</param>
        private static void CreateLog(LogLevel level, string typeName, string typeValue, Exception ex, string message, params object[] args)
        {
            LogEventInfo logEvent = new LogEventInfo(level, log.Name, message);
            logEvent.Parameters = args;
            logEvent.Properties["type_name"] = typeName;
            logEvent.Properties["type_value"] = typeValue;
            logEvent.Exception = ex;
            log.Log(logEvent);
        }
    }
}
