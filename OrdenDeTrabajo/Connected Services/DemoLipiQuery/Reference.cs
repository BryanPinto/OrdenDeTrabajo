﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebSolicitudes.DemoLipiQuery {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DemoLipiQuery.QueryFormSOASoap")]
    public interface QueryFormSOASoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/QueryCases", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode QueryCases(System.Xml.XmlNode xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/QueryEntities", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode QueryEntities(System.Xml.XmlNode xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/QueryCasesAsString", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string QueryCasesAsString(string sxml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/QueryEntitiesAsString", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string QueryEntitiesAsString(string sxml);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface QueryFormSOASoapChannel : WebSolicitudes.DemoLipiQuery.QueryFormSOASoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class QueryFormSOASoapClient : System.ServiceModel.ClientBase<WebSolicitudes.DemoLipiQuery.QueryFormSOASoap>, WebSolicitudes.DemoLipiQuery.QueryFormSOASoap {
        
        public QueryFormSOASoapClient() {
        }
        
        public QueryFormSOASoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public QueryFormSOASoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public QueryFormSOASoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public QueryFormSOASoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Xml.XmlNode QueryCases(System.Xml.XmlNode xml) {
            return base.Channel.QueryCases(xml);
        }
        
        public System.Xml.XmlNode QueryEntities(System.Xml.XmlNode xml) {
            return base.Channel.QueryEntities(xml);
        }
        
        public string QueryCasesAsString(string sxml) {
            return base.Channel.QueryCasesAsString(sxml);
        }
        
        public string QueryEntitiesAsString(string sxml) {
            return base.Channel.QueryEntitiesAsString(sxml);
        }
    }
}