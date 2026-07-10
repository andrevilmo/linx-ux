

   ----------------------------------------------------------------------
               README file for Web Markup Minifier: YUI 0.9.5

   ----------------------------------------------------------------------

          Copyright 2014 Andrey Taritsyn - http://www.taritsyn.ru
		  
		  
   ===========
   DESCRIPTION
   ===========   
   WebMarkupMin.Yui contains 2 minifier-adapters: YuiCssMinifier 
   (for minification of CSS code) and YuiJsMinifier (for minification of 
   JS code). These adapters perform minification using the YUI Compressor 
   for .NET (http://github.com/PureKrome/YUICompressor.NET).
   
   =============
   RELEASE NOTES
   =============
   Added support of the YUI Compressor for .NET 2.7.0.
   
   ====================
   POST-INSTALL ACTIONS
   ====================
   To make YuiCssMinifier is the default CSS minifier and YuiJsMinifier 
   is the default JS minifier, you need to make changes to the 
   Web.config file. 
   In defaultMinifier attribute of element 
   \configuration\webMarkupMin\core\css must be set value equal 
   to YuiCssMinifier, and in same attribute of element 
   \configuration\webMarkupMin\core\js - YuiJsMinifier.

   =============
   DOCUMENTATION
   =============
   See more detailed information on CodePlex -
   http://webmarkupmin.codeplex.com