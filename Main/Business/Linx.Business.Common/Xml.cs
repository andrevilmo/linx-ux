using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Linx.Tools;
using System.Collections;

namespace Linx.Business.Common
{
    public class Xml
    {
        /// <summary>
        /// Retorna o conteudo da tag solicitada
        /// </summary>
        /// <param name="xml">Documento XML onde a tag solicitada será procurada</param>
        /// <param name="tag">Nome da tag que será procurada</param>
        /// <returns></returns>
        public static string GetConteudoTag(string xml, string tag)
        {
            string valor = String.Empty;
            try
            {
                if (xml.IsNullOrEmpty()) return valor;

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                if (doc.GetElementsByTagName(tag).Count >= 1)
                {
                    var _node = doc.GetElementsByTagName(tag)[0];
                    if (_node.Value != null)
                        valor = _node.Value;
                    else
                    {
                        if (_node.FirstChild == _node.LastChild && doc.GetElementsByTagName(_node.FirstChild.Name)[0] != null)
                        {
                            var _node2 = doc.GetElementsByTagName(_node.FirstChild.Name)[0];
                            valor = _node2.FirstChild != _node2.LastChild ? _node.OuterXml : _node.InnerText;
                        }
                        else
                            valor = _node.FirstChild != _node.LastChild ? _node.OuterXml : _node.InnerText;
                    }
                }
            }
            catch
            {
            }
            return valor;
        }

        public static string GetConteudoTagCompleta(string xml, string tag)
        {
            string valor = String.Empty;
            try
            {
                if (xml.IsNullOrEmpty()) return valor;

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                if (doc.GetElementsByTagName(tag).Count >= 1)
                {
                    var _node = doc.GetElementsByTagName(tag)[0];
                    if (_node.Value != null)
                        valor = _node.Value;
                    else
                    {
                        if (_node.FirstChild == _node.LastChild && doc.GetElementsByTagName(_node.FirstChild.Name)[0] != null)
                            valor = _node.OuterXml;
                        else
                            valor = _node.FirstChild != _node.LastChild ? _node.OuterXml : _node.InnerText;
                    }
                }
            }
            catch
            {
            }
            return valor;
        }

        public static Boolean TagExists(string xml, string tag)
        {
            if (xml.IsNullOrEmpty()) return false;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            if (doc.GetElementsByTagName(tag).Count >= 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Retorna o conteudo do documento contendo a alteração solicitada
        /// </summary>
        /// <param name="xml">Documento XML onde a tag solicitada será procurada</param>
        /// <param name="tag">Nome da tag que será procurada</param>
        /// <param name="valor">Valor que será atribuido para o elemento solicitado</param>
        /// <returns></returns>
        public static string SetConteudoTag(string xml, string tag, string valor)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            if (doc.GetElementsByTagName(tag).Count >= 1)
            {
                var _node = doc.GetElementsByTagName(tag)[0];
                _node.Value = valor;
            }
            return doc.OuterXml;
        }

        /// <summary>
        /// Busca recursivamente o atributo passado como parâmetro
        /// </summary>
        /// <param name="xml">Documento XML onde o atributo solicitado será procurado</param>
        /// <param name="tag">Nome do atributo que será procurao</param>
        /// <returns></returns>
        public static string GetConteudoAtributoRecursivo(string xml, string parametro)
        {
            var doc = new XmlDocument();
            try
            {
                doc.LoadXml(xml);
            }
            catch (Exception)
            {
                return "";
            }

            return GetAtributo(doc, parametro);
        }

        private static string GetAtributo(XmlNode node, string nomeAtributo)
        {
            if (node == null)
                return null;
            else
            {
                if (node.Attributes != null && node.Attributes.Count > 0)
                    foreach (XmlAttribute atributo in node.Attributes)
                        if (atributo.Name == nomeAtributo)
                            return atributo.Value;
                if (node.FirstChild.NodeType == XmlNodeType.Text)
                    return GetAtributo(node.NextSibling, nomeAtributo);
                else
                    return GetAtributo(node.FirstChild, nomeAtributo);
            }
        }

        /// <summary>
        /// Retorna o conteudo do atributo solicitado
        /// </summary>
        /// <param name="xml">Documento XML onde o atributo solicitado será procurado</param>
        /// <param name="tag">Nome do atributo que será procurao</param>
        /// <returns></returns>
        public static string GetConteudoAtributo(string xml, string parametro)
        {
            string valor = String.Empty;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            if (doc.Attributes != null && doc.Attributes.Count >= 1)
            {
                foreach (XmlAttribute attr in doc.Attributes)
                {
                    if (attr.Name == parametro)
                    {
                        return attr.Value;
                    }
                }
            }
            else if (doc.FirstChild != null && doc.FirstChild.Attributes != null && doc.FirstChild.Attributes.Count >= 1)
            {
                foreach (XmlAttribute attr in doc.FirstChild.Attributes)
                {
                    if (attr.Name == parametro)
                    {
                        return attr.Value;
                    }
                }
            }
            return valor;
        }

        /// <summary>
        /// Retira os espaços entre as tags do XML
        /// </summary>
        /// <param name="xml">Documento XML onde que será retirado os espaços</param>
        /// <returns></returns>
        public static string GetXmlSemEspaco(string xml)
        {
            string valor = String.Empty;
            if (xml.IsNullOrEmpty()) return valor;

            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = false;
            doc.LoadXml(xml);

            byte[] utf8Bytes = Encoding.UTF8.GetBytes(doc.InnerXml.ToString());
            return Encoding.UTF8.GetString(utf8Bytes);
        }

        /// <summary>
        /// Retira entre as tags do XML
        /// Espaços
        /// Acentos
        /// Quebra de linha 
        /// Tabulações
        /// </summary>
        /// <param name="xml">String XML tratada</param>
        /// <returns></returns>
        public static string GetXmlSemEspacoSemAcento(string xml)
        {
            string xmlSemExpaco = String.Empty;
            string valor = String.Empty;
            if (String.IsNullOrEmpty(xml)) return valor;

            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = false;
            doc.LoadXml(xml);

            byte[] utf8Bytes = Encoding.UTF8.GetBytes(doc.InnerXml.ToString());
            xmlSemExpaco = Encoding.UTF8.GetString(utf8Bytes);

            xmlSemExpaco = xmlSemExpaco.Replace("\n", "")
                                       .Replace("\r", "")
                                       .Replace("\t", "")                                       
                                       .Replace("'", "");

            while (xmlSemExpaco.IndexOf("  ") >= 0)
                xmlSemExpaco = xmlSemExpaco.Replace("  ", " ");

            while (xmlSemExpaco.IndexOf("> ") >= 0)
                xmlSemExpaco = xmlSemExpaco.Replace("> ", ">");

            while (xmlSemExpaco.IndexOf(" <") >= 0)
                xmlSemExpaco = xmlSemExpaco.Replace(" <", "<");

            if (string.IsNullOrEmpty(xmlSemExpaco))
                return "";
            else
            {
                byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(xmlSemExpaco);
                return System.Text.Encoding.UTF8.GetString(bytes);
            }
        }

        /// <summary>
        /// Altera caracteres especiais do valor de uma tag
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static string AjustaValorTag(string valor)
        {
            return valor.Replace("&", "&amp;")
                        .Replace("<", "&lt;")
                        .Replace(">", "&gt;")
                        .Replace("\"", "&quot;")
                        .Replace("'", "&#39;")
                        .Replace("§", "&sect;")
                        .Replace("?", "&#63;");
        }

        public static XElement SerializarEntidade(Type tipo, object entidade)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            XmlSerializer s = new XmlSerializer(tipo);

            StringWriter sw = new StringWriter();
            s.Serialize(sw, entidade, ns);

            XElement elemento = XElement.Parse(sw.ToString());

            //TODO: Validar regras do elemento (Retirar tags desnecessárias de acordo com o tipo enviado) Sidney

            elemento = ModificaNamespace(elemento, "http://www.portalfiscal.inf.br/nfe");

            return elemento;
        }

        private static XElement ModificaNamespace(XElement elemento, XNamespace nsSefaz)
        {
            XElement novo = new XElement(
                nsSefaz + elemento.Name.LocalName,
                elemento.Attributes().Select(p => new XElement(nsSefaz + p.Name.LocalName, p.Value)));

            if (elemento.HasElements)
            {
                for (int i = 0; i < elemento.Elements().Count(); i++)
                {
                    XElement item = elemento.Elements().ElementAt(i);

                    if (item.HasElements)
                        novo.Add(ModificaNamespace(item, nsSefaz));
                    else
                        novo.Add(new XElement(nsSefaz + item.Name.LocalName, AjustaValorTag(item.Value)));
                }
            }

            return novo;
        }
    }
}
