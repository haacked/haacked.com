---
title: Datatable as a method parameter
tags: [code]
redirect_from: "/archive/2004/02/03/DataTableAsAMethodParameter.aspx/"
---

I'm working with a third party component (I will not name the guilty
party) that has a method with the following signature and implementation
(with no overrides):

```csharp
public void doSomething(DataTable table)
{
   for(int i = 0; i < table.Rows.Count; i++)
   {
      DataRow row = table.Rows[i];
      //Do Something with row...
   }
}
```

What is the problem with this method?

In my opinion, it is an example of poor class design. Suppose the user
of this class wanted to have the component process the rows of the
DataTable in a particular order. How would one accomplish that?

If you take a look at the DataTable class members, you won't find a
Sort() method. The reason is that a DataTable cannot be sorted. The
correct way to sort a table is through a DataView. The DataView is
simply a view (appropriately enough) of the underlying data within a
DataTable. You can have multiple views on a single DataTable and sort
and filter all you want on the views, but the underlying data does not
change. For example, a naive approach (and one that I took) was to apply
a sort on the DefaultView of the DataTable. I tried this before I knew
the method's internal implementation. This approach failed due to the
fact that the method completely ignores the view of the DataTable.

I would suggest that the method be changed to iterate over the
DataTable's default view. That's what it is there for. However, the
author may have decided to iterate over the DataTable for performance
reasons. If so, a better design would have allowed for an override
method that takes in a DataView and uses the view to iterate. Like so:

public void doSomething(DataView tableView)
{
   for(int i = 0; i \< tableView.Count; i++)
   {
      DataRowView row = tableView[i];
      //Now Do Something with row...
   }
}
```

This results in improved flexibilty for the user of the class.
Thankfully, the author promised to include this in the next version of
his component and send me a preview copy.

