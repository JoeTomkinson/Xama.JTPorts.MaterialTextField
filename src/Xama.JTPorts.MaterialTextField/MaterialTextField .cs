using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Support.V4.Content;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
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

        protected void Init()
        {
            inputMethodManager = (InputMethodManager)Context.GetSystemService(Context.InputMethodService);
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
                int heightInitial = Context.Resources.GetDimensionPixelOffset(Resource.Dimension.mtf_cardHeight_final);

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
                    inputMethodManager.HideSoftInputFromInputMethod(editText.WindowToken, 0);
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

        public View GetCard()
        {
            return card;
        }

        public TextView GetLabel()
        {
            return label;
        }

        public ImageView GetImage()
        {
            return image;
        }

        public EditText GetEditText()
        {
            return editText;
        }

        public ViewGroup GetEditTextLayout()
        {
            return editTextLayout;
        }

        public bool IsExpanded()
        {
            return expanded;
        }

        public void SetHasFocus(bool hasFocus)
        {
            this.hasFocus = hasFocus;

            if (hasFocus)
            {
                Expand();

                Runnable runnable = new Runnable(() =>
                {
                    editText.RequestFocusFromTouch();
                    inputMethodManager.ShowSoftInput(editText, 0);
                });

                editText.PostDelayed(runnable, 300);
            }
            else
            {
                Reduce();
            }
        }

        protected void HandleAttributes(Context context, IAttributeSet attrs)
        {
            try
            {
                TypedArray styledAttrs = context.ObtainStyledAttributes(attrs, Resource.Styleable.MaterialTextField);
                {
                    ANIMATION_DURATION = styledAttrs.GetInteger(Resource.Styleable.MaterialTextField_mtf_animationDuration, 400);
                }
                {
                    OPEN_KEYBOARD_ON_FOCUS = styledAttrs.GetBoolean(Resource.Styleable.MaterialTextField_mtf_openKeyboardOnFocus, false);
                }
                {
                    labelColor = styledAttrs.GetColor(Resource.Styleable.MaterialTextField_mtf_labelColor, -1);
                }
                {
                    imageDrawableId = styledAttrs.GetResourceId(Resource.Styleable.MaterialTextField_mtf_image, -1);
                }
                {
                    cardCollapsedHeight = styledAttrs.GetDimensionPixelOffset(Resource.Styleable.MaterialTextField_mtf_cardCollapsedHeight, context.Resources.GetDimensionPixelOffset(Resource.Dimension.mtf_cardHeight_initial));
                }
                {
                    hasFocus = styledAttrs.GetBoolean(Resource.Styleable.MaterialTextField_mtf_hasFocus, false);
                }
                {
                    backgroundColor = styledAttrs.GetColor(Resource.Styleable.MaterialTextField_mtf_backgroundColor, -1);
                }

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

            editTextLayout = (ViewGroup)FindViewById(R.id.mtf_editTextLayout);
            RemoveView(editText);
            editTextLayout.AddView(editText);

            label = (TextView)FindViewById(R.id.mtf_label);
            ViewCompat.SetPivotX(label, 0);
            ViewCompat.SetPivotY(label, 0);

            if (editText.Hint != null)
            {
                label.Text = editText.Hint;
                editText.Hint = "";
            }

            card = FindViewById(R.id.mtf_card);

            if (backgroundColor != -1)
            {
                card.SetBackgroundColor(backgroundColor);
            }

            int expandedHeight = Context.Resources.GetDimensionPixelOffset(R.dimen.mtf_cardHeight_final);
            int reducedHeight = cardCollapsedHeight;

            reducedScale = (float)(reducedHeight * 1.0 / expandedHeight);
            ViewCompat.SetScaleY(card, reducedScale);
            ViewCompat.SetPivotY(card, expandedHeight);

            image = (ImageView)FindViewById(R.id.mtf_image);
            ViewCompat.SetAlpha(image, 0);
            ViewCompat.SetScaleX(image, 0.4f);
            ViewCompat.SetScaleY(image, 0.4f);

            ViewCompat.SetAlpha(editText, 0f);
            editText.SetBackgroundColor(Color.Transparent);

            //labelTopMargin = FrameLayout.LayoutParams.class.cast(label.getLayoutParams()).topMargin;

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
                this.label.SetTextColor(labelColor);
            }

            if (imageDrawableId != -1)
            {
                this.image.SetImageDrawable(ContextCompat.GetDrawable(Context, imageDrawableId));
            }
        }

        public void OnAnimationUpdate(View view)
        {
            //TODO: do this
            throw new System.NotImplementedException();
        }

        public void OnAnimationCancel(View view)
        {
            //TODO: do this
            throw new System.NotImplementedException();
        }

        public void OnAnimationEnd(View view)
        {
            //TODO: do this
            throw new System.NotImplementedException();
        }

        public void OnAnimationStart(View view)
        {
            //TODO: do this
            throw new System.NotImplementedException();
        }
    }
}
