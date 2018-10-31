//Generated by Azos.Sky.Clients.Tools.SkyGluecCompiler

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Azos.Glue;
using Azos.Glue.Protocol;


namespace Azos.Sky.Clients
{
// This implementation needs @Azos.@Sky.@Contracts.@IZoneLogReceiverClient, so
// it can be used with ServiceClientHub class

  ///<summary>
  /// Client for glued contract Azos.Sky.Contracts.IZoneLogReceiver server.
  /// Each contract method has synchronous and asynchronous versions, the later denoted by 'Async_' prefix.
  /// May inject client-level inspectors here like so:
  ///   client.MsgInspectors.Register( new YOUR_CLIENT_INSPECTOR_TYPE());
  ///</summary>
  public class ZoneLogReceiver : ClientEndPoint, @Azos.@Sky.@Contracts.@IZoneLogReceiverClient
  {

  #region Static Members

     private static TypeSpec s_ts_CONTRACT;
     private static MethodSpec @s_ms_SendLog_0;

     //static .ctor
     static ZoneLogReceiver()
     {
         var t = typeof(@Azos.@Sky.@Contracts.@IZoneLogReceiver);
         s_ts_CONTRACT = new TypeSpec(t);
         @s_ms_SendLog_0 = new MethodSpec(t.GetMethod("SendLog", new Type[]{ typeof(@System.@String), typeof(@System.@String), typeof(@@Log.@Message[]) }));
     }
  #endregion

  #region .ctor
     public ZoneLogReceiver(string node, Binding binding = null) : base(node, binding) { ctor(); }
     public ZoneLogReceiver(Node node, Binding binding = null) : base(node, binding) { ctor(); }
     public ZoneLogReceiver(IGlue glue, string node, Binding binding = null) : base(glue, node, binding) { ctor(); }
     public ZoneLogReceiver(IGlue glue, Node node, Binding binding = null) : base(glue, node, binding) { ctor(); }

     //common instance .ctor body
     private void ctor()
     {

     }

  #endregion

     public override Type Contract
     {
       get { return typeof(@Azos.@Sky.@Contracts.@IZoneLogReceiver); }
     }



  #region Contract Methods

         ///<summary>
         /// Synchronous invoker for  'Azos.Sky.Contracts.IZoneLogReceiver.SendLog'.
         /// This is a two-way call per contract specification, meaning - the server sends the result back either
         ///  returning '@System.@Int32' or WrappedExceptionData instance.
         /// ClientCallException is thrown if the call could not be placed in the outgoing queue.
         /// RemoteException is thrown if the server generated exception during method execution.
         ///</summary>
         public @System.@Int32 @SendLog(@System.@String  @host, @System.@String  @appName, @@Log.@Message[]  @data)
         {
            var call = Async_SendLog(@host, @appName, @data);
            return call.GetValue<@System.@Int32>();
         }

         ///<summary>
         /// Asynchronous invoker for  'Azos.Sky.Contracts.IZoneLogReceiver.SendLog'.
         /// This is a two-way call per contract specification, meaning - the server sends the result back either
         ///  returning no exception or WrappedExceptionData instance.
         /// CallSlot is returned that can be queried for CallStatus, ResponseMsg and result.
         ///</summary>
         public CallSlot Async_SendLog(@System.@String  @host, @System.@String  @appName, @@Log.@Message[]  @data)
         {
            var request = new RequestAnyMsg(s_ts_CONTRACT, @s_ms_SendLog_0, false, RemoteInstance, new object[]{@host, @appName, @data});
            return DispatchCall(request);
         }


  #endregion

  }//class
}//namespace
