#if WINDOWS
using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
#endif

namespace NovaEditor;

/// <summary>Handles changing the cursor when hovering over elements.</summary>
public static class CursorManager
{
    /*********
    ** Public Methods
    *********/
    /// <summary>Sets the cursor when hovering on a view.</summary>
    /// <param name="view">The view that hovering over should change the cursor.</param>
    /// <param name="cursor">The cursor that should be set when hovering.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="view"/> is <see langword="null"/>.</exception>
    public static void SetHoverCursor(View view, Cursor cursor)
    {
        ArgumentNullException.ThrowIfNull(view);

#if WINDOWS
        // setting the cursor each time the pointer enters the element is unnecesaary, but just adding once the handler
        // had been set was resulting in AccessViolationExceptions.

        // this method for setting the cursor was from https://github.com/dotnet/maui/issues/4552#issuecomment-1439554727
        // in the example it was also applied each time the pointer entered so it'll just stay like this for now.
        // after all, this is only temporary until MAUI has an official way of setting the cursor for an element
        var pointerGestureRecogniser = new PointerGestureRecognizer();
        pointerGestureRecogniser.PointerEntered += (_, _) => SetHoverCursorWindows(view, cursor);
        view.GestureRecognizers.Add(pointerGestureRecogniser);
#endif

        // TODO: mac
    }


    /*********
    ** Private Methods
    *********/
#if WINDOWS
    /// <summary>Sets the cursor when hovering on a view in Windows.</summary>
    /// <param name="view">The element that hovering over should change the cursor.</param>
    /// <param name="cursor">The cursor that should be set when hovering.</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="cursor"/> isn't a valid enum value.</exception>
    private static void SetHoverCursorWindows(View view, Cursor cursor)
    {
        var inputSystemCursorShape = cursor switch
        {
            Cursor.ResiveHorizontal => InputSystemCursorShape.SizeWestEast,
            Cursor.ResiveVertical => InputSystemCursorShape.SizeNorthSouth,
            _ => throw new ArgumentException("Invalid value.", nameof(cursor))
        };

        typeof(UIElement).GetMethod("set_ProtectedCursor", BindingFlags.NonPublic | BindingFlags.Instance)
            .Invoke(view.Handler.PlatformView, new[] { InputSystemCursor.Create(inputSystemCursorShape) });
    }
#endif
}
