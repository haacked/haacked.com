---
title: DataGrid With a Title Row
date: 2005-04-19 -0800 9:00 AM
tags: [data]
redirect_from: "/archive/2005/04/18/DataGridWithATitleRow.aspx/"
---

One thing I've found annoying at times with the DataGrid control is
there's no way to specify a title to be displayed above the headers.
Being lazy, I often resorted to adding a label above the data grid
followed by a br tag.

But no longer! I wanted to have the title display in its own row within
the data grid structure so I created a custom data grid control that
does just that. See the example below for how it renders.

```html
<table id="gridtemplated" style="border-collapse: collapse;" border="1" cellspacing="0" rules="all">
    <tbody>
        <tr class="grid_title">
            <td colspan="3" align="center">This is the Title</td>
        </tr>
        <tr class="grid_header">
            <td>Column 1</td>
            <td>Column 2</td>
            <td>Column 3</td>
        </tr>
        <tr>
            <td>
                <span>See</span>
            </td>
            <td>Spot</td>
            <td>
                <span>Run</span>
            </td>
        </tr>
        <tr>
            <td>
                <span>Run</span>
            </td>
            <td>Spot</td>
            <td>
                <span>Run!</span>
            </td>
        </tr>
        <tr>
            <td>
                <span>Flee</span>
            </td>
            <td>the</td>
            <td>
                <span>Maniacs</span>
            </td>
        </tr>
    </tbody>
</table>
```

The key to this is to override the OnItemCreated method and set my own
rendering method for the header item.

```csharp
/// <summary>
/// Assigns our own render method for the header item.
/// </summary>
/// <param name="e"> E. </param>
protected override void OnItemCreated(DataGridItemEventArgs e)
{
  if (ListItemType.Header == e.Item.ItemType && Title != null &&
Title.Length > 0)
  {
    e.Item.SetRenderMethodDelegate( new RenderMethod(RenderTitle));
  }
  else
  {
    base.OnItemCreated(e);
  }
}
```

When ASP.NET is ready to render the Header, it'll call my method instead
which is named RenderTitle.

```csharp
/// <summary>
/// Renders the title as its own row.
/// </summary>
/// <param name="writer"> Writer. </param>
/// <param name="ctl"> CTL. </param>
protected virtual void RenderTitle(HtmlTextWriter writer, Control ctl)
{
   // TR is on the stack writer's stack at this point...
   writer.AddAttribute(
       "colspan",
       this.Columns.Count.ToString(CultureInfo.InvariantCulture));

   writer.AddAttribute("align", "center");
   writer.RenderBeginTag("TD");
   writer.Write(Title);
   writer.RenderEndTag(); // Writes </TD>
   writer.RenderEndTag(); // Writes </TR>

   // Now we add the header attributes we
   // copied.
   this .HeaderStyle.AddAttributesToRender(writer);
   writer.RenderBeginTag("TR");

   //Render the cells for the header row.
   foreach (Control control in ctl.Controls)
   {
     control.RenderControl(writer);
   }

   // We don't need to write the </TR>.
   // The grid will do that for us.
}
```

The snippet shown here will style the title row the same as the header
style. In my actual control, I defined a property named TitleCssClass to
enable you to define your own Css class to use to style the title row.
This required me to do a bit of hacking so that the HeaderStyle gets
removed from the render stack and then gets added back later when
rendering the Header row. If that makes no sense, you'll see what I
mean. I've put the code for the control
[here](https://haacked.com/code/TitledDataGrid.zip).
