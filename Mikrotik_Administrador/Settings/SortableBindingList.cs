using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Mikrotik_Administrador.Settings
{
    public class SortableBindingList<T> : BindingList<T>
    {
        public SortableBindingList(IList<T> list) : base(list) { }

        protected override bool SupportsSortingCore => true;

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            // Usamos 'as' para seguridad y verificamos la lista interna
            var itemsList = this.Items as List<T>;

            if (itemsList == null && this.Items != null)
            {
                // Si no es List<T>, forzamos la conversión una sola vez
                itemsList = new List<T>(this.Items);
                this.Items.Clear();
                foreach (var item in itemsList) this.Items.Add(item);
            }

            if (itemsList != null)
            {
                var pc = new PropertyComparer<T>(prop, direction);
                itemsList.Sort(pc);

                // ESTA LÍNEA ES VITAL: Avisa al Grid que la lista se movió
                this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
        }
        private class PropertyComparer<TInner> : IComparer<TInner>
        {
            private readonly PropertyDescriptor _prop;
            private readonly ListSortDirection _direction;

            public PropertyComparer(PropertyDescriptor prop, ListSortDirection direction)
            {
                _prop = prop;
                _direction = direction;
            }

            public int Compare(TInner x, TInner y)
            {
                try
                {
                    var xVal = _prop.GetValue(x);
                    var yVal = _prop.GetValue(y);

                    if (xVal == null && yVal == null) return 0;
                    if (xVal == null) return -1;
                    if (yVal == null) return 1;

                    if (xVal is IComparable comparable)
                    {
                        int result = comparable.CompareTo(yVal);
                        return (_direction == ListSortDirection.Ascending) ? result : -result;
                    }

                    // Si no es comparable (ej. un objeto complejo), comparamos su texto
                    return string.Compare(xVal.ToString(), yVal.ToString()) * (_direction == ListSortDirection.Ascending ? 1 : -1);
                }
                catch
                {
                    return 0; // Evita que la app truene si algo sale mal
                }
            }
        }
        public void ApplySort(PropertyDescriptor prop, ListSortDirection direction)
        {
            ApplySortCore(prop, direction);
        }
    }
}