using Android.Content;
using Android.Content.Res;
using Android.Graphics;
//using Android.Support.V4.Content;
//using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidX.Core.Content;
using AndroidX.Core.View;
using Java.Lang;

namespace Xama.JTPorts.MaterialTextField
{
    public class MaterialTextField : FrameLayout, IViewPropertyAnimatorUpdateListener, IViewPropertyAnimatorListener
    {
        protected InputMethodManager inputMethodManager;
        protected TextView label;
        protected View card;
        protected ImageView image;
        protected EditText editText;
        protected ViewGroup editTextLayout;
        protected int labelTopMargin = -1;
        protected bool expanded = false;
        protected int ANIMATION_DURATION = -1;
        protected bool OPEN_KEYBOARD_ON_FOCUS = true;
        protected int labelColor = -1;
        protected int imageDrawableId = -1;
        protected int cardCollapsedHeight = -1;
        protected bool hasFocus = false;
        protected int backgroundColor = -1;
        protected float reducedScale = 0.2f;
        protected int heightInitial;

        // public properties

        public View Card => card;
        public TextView Label => label;
        public ImageView Image => image;
        public EditText EditText => editText;
        public ViewGroup EditTextLayout => editTextLayout;
        public bool IsExpanded => expanded;

        public MaterialTextField(Context context) : base(context)
        {
            Init();
        }

        public MaterialTextField(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            HandleAttributes(context, attrs);
            Init();
        }

        public MaterialTextField(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            HandleAttributes(context, attrs);
            Init();
        }

        public void Toggle()
        {
            if (expanded)
            {
                Reduce();
            }
            else
            {
                Expand();
            }
        }

        public void Reduce()
        {
            if (expanded)
            {
                heightInitial = Context.Resources.GetDimensionPixelOffset(Resource.Dimension.mtf_cardHeight_final);

                ViewCompat.Animate(label)
                    .Alpha(1)
                    .ScaleX(1)
                    .ScaleY(1)
                    .TranslationY(0)
                    .SetDuration(ANIMATION_DURATION);

                ViewCompat.Animate(image)
                    .Alpha(0)
                    .ScaleX(0.4f)
                    .ScaleY(0.4f)
                    .SetDuration(ANIMATION_DURATION);

                ViewCompat.Animate(editText)
                    .Alpha(1f)
                    .SetUpdateListener(this)
                    .SetDuration(ANIMATION_DURATION)
                    .SetListener(this);

                ViewCompat.Animate(card)
                    .ScaleY(reducedScale)
                    .SetDuration(ANIMATION_DURATION);

                if (editText.HasFocus)
                {
                    inputMethodManager.HideSoftInputFromWindow(editText.WindowToken, HideSoftInputFlags.None);
                    //inputMethodManager.HideSoftInputFromInputMethod(editText.WindowToken, 0);
                    editText.ClearFocus();
                }

                expanded = false;
            }
        }

        public void Expand()
        {
            if (!expanded)
            {
                ViewCompat.Animate(editText)
                    .Alpha(1f)
                    .SetDuration(ANIMATION_DURATION);

                ViewCompat.Animate(card)
                    .ScaleY(1f)
                    .SetDuration(ANIMATION_DURATION);

                ViewCompat.Animate(label)
                    .Alpha(0.4f)
                    .ScaleX(0.7f)
                    .ScaleY(0.7f)
                    .TranslationY(-labelTopMargin)
                    .SetDuration(ANIMATION_DURATION);

                ViewCompat.Animate(image)
                    .Alpha(1f)
                    .ScaleX(1f)
                    .ScaleY(1f)
                    .SetDuration(ANIMATION_DURATION);

                if (editText != null)
                {
                    editText.RequestFocus();
                }

                if (OPEN_KEYBOARD_ON_FOCUS)
                {
                    inputMethodManager.ShowSoftInput(editText, ShowFlags.Implicit);
                }

                expanded = true;
            }
        }

        public void SetBackgroundColor(int color)
        {
            this.backgroundColor = color;
        }

        public int GetBackgroundColor()
        {
            return this.backgroundColor;
        }

        public void SetHasFocus(bool hasFocus)
        {
            this.hasFocus = hasFocus;

            if (hasFocus)
            {
                Expand();

                editText.PostDelayed(() =>
                {
                    editText.RequestFocusFromTouch();
                    inputMethodManager.ShowSoftInput(editText, 0);
                    // added as keyboard doesnt auto show on edit text visibility.
                    inputMethodManager.ToggleSoftInput(ShowFlags.Forced, 0);

                }, 300);
            }
            else
            {
                Reduce();
            }
        }

        // listener methods

        public void OnAnimationUpdate(View view)
        {
            float value = view.Alpha;
            //percentage
            card.LayoutParameters.Height = (int)(value * (heightInitial - cardCollapsedHeight) + cardCollapsedHeight);
            card.RequestLayout();
        }

        public void OnAnimationCancel(View view)
        {
            //
        }

        public void OnAnimationEnd(View view)
        {
            if (!expanded)
            {
                editText.Visibility = ViewStates.Invisible;
            }
        }

        public void OnAnimationStart(View view)
        {
            if (expanded)
            {
                editText.Visibility = ViewStates.Visible;
            }
        }

        // internal library methods 

        protected void Init()
        {
            inputMethodManager = (InputMethodManager)Context.GetSystemService(Context.InputMethodService);
            ViewTreeObserver.GlobalFocusChange += ViewTreeObserver_GlobalFocusChange;
        }

        private void ViewTreeObserver_GlobalFocusChange(object sender, ViewTreeObserver.GlobalFocusChangeEventArgs e)
        {
            if (e.NewFocus != Card && e.NewFocus != Label && e.NewFocus != Image && e.NewFocus != EditText && e.NewFocus != EditTextLayout)
            {
                if (string.IsNullOrWhiteSpace(EditText.Text))
                {
                    Reduce();
                }
            }
        }

        protected void HandleAttributes(Context context, IAttributeSet attrs)
        {
            try
            {
                TypedArray styledAttrs = context.ObtainStyledAttributes(attrs, Resource.Styleable.MaterialTextField);

                ANIMATION_DURATION = styledAttrs.GetInteger(Resource.Styleable.MaterialTextField_mtf_animationDuration, 400);
                OPEN_KEYBOARD_ON_FOCUS = styledAttrs.GetBoolean(Resource.Styleable.MaterialTextField_mtf_openKeyboardOnFocus, true);
                labelColor = styledAttrs.GetColor(Resource.Styleable.MaterialTextField_mtf_labelColor, -1);
                imageDrawableId = styledAttrs.GetResourceId(Resource.Styleable.MaterialTextField_mtf_image, -1);
                cardCollapsedHeight = styledAttrs.GetDimensionPixelOffset(Resource.Styleable.MaterialTextField_mtf_cardCollapsedHeight, context.Resources.GetDimensionPixelOffset(Resource.Dimension.mtf_cardHeight_initial));
                hasFocus = styledAttrs.GetBoolean(Resource.Styleable.MaterialTextField_mtf_hasFocus, false);
                backgroundColor = styledAttrs.GetColor(Resource.Styleable.MaterialTextField_mtf_backgroundColor, -1);

                styledAttrs.Recycle();
            }
            catch (Exception e)
            {
                e.PrintStackTrace();
            }
        }

        protected EditText FindEditTextChild()
        {
            if (ChildCount > 0 && GetChildAt(0) is EditText)
            {
                return (EditText)GetChildAt(0);
            }
            return null;
        }

        protected override void OnFinishInflate()
        {
            base.OnFinishInflate();

            editText = FindEditTextChild();
            if (editText == null)
            {
                return;
            }

            AddView(LayoutInflater.From(Context).Inflate(Resource.Layout.mtf_layout, this, false));

            editTextLayout = (ViewGroup)FindViewById(Resource.Id.mtf_editTextLayout);
            RemoveView(editText);
            editTextLayout.AddView(editText);

            // replaced ViewCompat.SetPivotX and Y as they are deprecated.
            label = (TextView)FindViewById(Resource.Id.mtf_label);
            label.PivotX = 0;
            label.PivotY = 0;

            if (editText.Hint != null)
            {
                label.Text = editText.Hint;
                editText.Hint = "";
            }

            card = FindViewById(Resource.Id.mtf_card);

            if (backgroundColor != -1)
            {
                Color c;

                try
                {
                    c = new Color(this.backgroundColor);
                }
                catch
                {
                    c = Color.White;
                }

                card.SetBackgroundColor(c);
            }

            int expandedHeight = Context.Resources.GetDimensionPixelOffset(Resource.Dimension.mtf_cardHeight_final);
            int reducedHeight = cardCollapsedHeight;

            reducedScale = (float)(reducedHeight * 1.0 / expandedHeight);
            card.ScaleY = reducedScale;
            card.PivotY = expandedHeight;

            image = (ImageView)FindViewById(Resource.Id.mtf_image);
            image.Alpha = 0;
            image.ScaleX = 0.4f;
            image.ScaleY = 0.4f;

            editText.Alpha = 0f;

            editText.SetBackgroundColor(Color.Transparent);

            // Converted below code, so needs testing.
            // FrameLayout.LayoutParams.class.cast(label.getLayoutParams()).topMargin;
            ViewGroup.MarginLayoutParams lp = (ViewGroup.MarginLayoutParams)label.LayoutParameters;
            labelTopMargin = lp.TopMargin;

            CustomizeFromAttributes();

            this.Click += (s, e) =>
            {
                Toggle();
            };

            SetHasFocus(hasFocus);
        }

        protected void CustomizeFromAttributes()
        {
            if (labelColor != -1)
            {
                Color c;

                try
                {
                    c = new Color(this.labelColor);
                }
                catch
                {
                    c = Color.Black;
                }

                this.label.SetTextColor(c);
            }

            if (imageDrawableId != -1)
            {
                this.image.SetImageDrawable(ContextCompat.GetDrawable(Context, imageDrawableId));
            }
        }
    }
}
