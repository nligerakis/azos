/*<FILE_LICENSE>
 * Azos (A to Z Application Operating System) Framework
 * The A to Z Foundation (a.k.a. Azist) licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
</FILE_LICENSE>*/

using System;
using System.Diagnostics;

using Azos.Scripting;

using Azos.Serialization.JSON;
using Azos.Collections;
using System.IO;
using Azos.Data;
using Azos.Time;
using Azos.Financial;
using System.Collections.Generic;

namespace Azos.Tests.Nub.Serialization
{
  [Runnable]
  public class JSONToDocExtendedTests
  {

    public struct CustomStructType : IJSONWritable, IJSONReadable
    {
      public CustomStructType(string text)
      {
        Text = text;
        Length = text==null? 0: text.Length;
      }

      public string Text;
      public int Length;

      public bool ReadAsJSON(object data, bool fromUI, JSONReader.NameBinding? nameBinding)
      {
        if (data==null) return false;

        var str = data as string;
        if (str==null) str = data.ToString();

        Text = str;
        Length = str.Length;
        return true;
      }

      public void WriteAsJSON(TextWriter wri, int nestingLevel, JSONWritingOptions options = null)
      {
        JSONWriter.EncodeString(wri, Text, options);
      }
    }

    public class CustomDoc1 : TypedDoc
    {
      [Field] public string ID{ get;set;}
      [Field] public CustomStructType Data{ get;set;}
    }

    public class CustomDoc2 : TypedDoc
    {
      [Field] public string ID { get; set; }
      [Field] public CustomStructType? Data { get; set; }
    }

    public class CustomDoc3 : TypedDoc
    {
      [Field] public string ID { get; set; }
      [Field] public CustomStructType[] Data { get; set; }
    }

    public class CustomDoc4 : TypedDoc
    {
      [Field] public string ID { get; set; }
      [Field] public List<CustomStructType> Data { get; set; }
    }


    [Run]
    public void CustomWritableReadable_1()
    {
      var d1 = new CustomDoc1{ ID = "sss", Data =  new CustomStructType("Custom string 1") };

      var json = d1.ToJSON(JSONWritingOptions.PrettyPrintRowsAsMap);
      Console.WriteLine(json);
      var jsonMap = json.JSONToDataObject() as JSONDataMap;

      var d2 = new CustomDoc1();
      JSONReader.ToDoc(d2, jsonMap);

      Aver.AreEqual(d1.Data.Text, d2.Data.Text);
      Aver.AreEqual(d1.Data.Length, d2.Data.Length);
    }

    [Run]
    public void CustomWritableReadable_2()
    {
      var d1 = new CustomDoc2 { ID = "sss", Data = new CustomStructType("Custom string 1") };

      var json = d1.ToJSON(JSONWritingOptions.PrettyPrintRowsAsMap);
      Console.WriteLine(json);
      var jsonMap = json.JSONToDataObject() as JSONDataMap;

      var d2 = new CustomDoc2();
      JSONReader.ToDoc(d2, jsonMap);

      Aver.AreEqual(d1.Data.Value.Text, d2.Data.Value.Text);
      Aver.AreEqual(d1.Data.Value.Length, d2.Data.Value.Length);
    }

    [Run]
    public void CustomWritableReadable_3()
    {
      var d1 = new CustomDoc3 { ID = "sss", Data = new CustomStructType[]{ new CustomStructType("Custom string 1")} };

      var json = d1.ToJSON(JSONWritingOptions.PrettyPrintRowsAsMap);
      Console.WriteLine(json);
      var jsonMap = json.JSONToDataObject() as JSONDataMap;

      var d2 = new CustomDoc3();
      JSONReader.ToDoc(d2, jsonMap);

      Aver.AreEqual(d1.Data[0].Text, d2.Data[0].Text);
      Aver.AreEqual(d1.Data[0].Length, d2.Data[0].Length);
    }

    [Run]
    public void CustomWritableReadable_4()
    {
      var d1 = new CustomDoc4 { ID = "sss", Data = new List<CustomStructType> { new CustomStructType("Custom string 1") } };

      var json = d1.ToJSON(JSONWritingOptions.PrettyPrintRowsAsMap);
      Console.WriteLine(json);
      var jsonMap = json.JSONToDataObject() as JSONDataMap;

      var d2 = new CustomDoc4();
      JSONReader.ToDoc(d2, jsonMap);

      Aver.AreEqual(d1.Data[0].Text, d2.Data[0].Text);
      Aver.AreEqual(d1.Data[0].Length, d2.Data[0].Length);
    }



    public class WithVariousStructsDoc : TypedDoc
    {
      [Field] public GDID       Gdid{ get;set;}
      [Field] public GDIDSymbol GdidSymbol { get; set; }
      [Field] public Guid       Guid { get; set; }
      [Field] public Atom       Atom { get; set; }
      [Field] public TimeSpan   Timespan { get; set; }
      [Field] public DateTime   DateTime { get; set; }
      [Field] public NLSMap     Nls { get; set; }
      [Field] public DateRange  DateRange { get; set; }
      [Field] public Amount     Amount { get; set; }
      [Field] public StringMap  StringMap { get; set; }
    }

    public class WithVariousNullableStructsDoc : TypedDoc
    {
      [Field] public GDID?       Gdid        { get; set; }
      [Field] public GDIDSymbol? GdidSymbol  { get; set; }
      [Field] public Guid?       Guid        { get; set; }
      [Field] public Atom?       Atom        { get; set; }
      [Field] public TimeSpan?   Timespan    { get; set; }
      [Field] public DateTime?   DateTime    { get; set; }
      [Field] public NLSMap?     Nls         { get; set; }
      [Field] public DateRange?  DateRange   { get; set; }
      [Field] public Amount?     Amount      { get; set; }
      [Field] public StringMap   StringMap   { get; set; }

      [Field] public GDID     GdidArray   { get; set; }
      [Field] public string[] StringArray { get; set; }
      [Field] public char[]   CharArray   { get; set; }
    }


    [Run]
    public void Test_WithVariousNullableStructsDoc_GDID()
    {
      var d1 = new WithVariousNullableStructsDoc{ Gdid = new GDID(1,2,3)   };
      var json = d1.ToJSON(JSONWritingOptions.PrettyPrintRowsAsMap);
      Console.WriteLine(json);
      var map = json.JSONToDataObject() as JSONDataMap;

      var d2 = new WithVariousNullableStructsDoc();
      JSONReader.ToDoc(d2, map);

      Aver.AreEqual(d1.Gdid, d2.Gdid);
    }

    [Run]
    public void Test_WithVariousNullableStructsDoc_GDIDSymbol()
    {
      var d1 = new WithVariousNullableStructsDoc { GdidSymbol = new GDIDSymbol(new GDID(1, 2, 3), "abrkadabra") };
      var json = d1.ToJSON(JSONWritingOptions.PrettyPrintRowsAsMap);
      Console.WriteLine(json);
      var map = json.JSONToDataObject() as JSONDataMap;

      var d2 = new WithVariousNullableStructsDoc();
      JSONReader.ToDoc(d2, map);

      Aver.AreEqual(d1.GdidSymbol, d2.GdidSymbol);
    }

    [Run]
    public void Test_WithVariousNullableStructsDoc_Guid()
    {
      var d1 = new WithVariousNullableStructsDoc { Guid = Guid.NewGuid() };
      var json = d1.ToJSON(JSONWritingOptions.PrettyPrintRowsAsMap);
      Console.WriteLine(json);
      var map = json.JSONToDataObject() as JSONDataMap;

      var d2 = new WithVariousNullableStructsDoc();
      JSONReader.ToDoc(d2, map);

      Aver.AreEqual(d1.Guid, d2.Guid);
    }
    //=================================











    //==================================
  }//class
}//namespace