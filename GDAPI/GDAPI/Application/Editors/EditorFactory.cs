﻿using GDAPI.Objects.General;
using GDAPI.Objects.GeometryDash.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GDAPI.Application.Editors
{
    public abstract class EditorFactory
    {
        protected static readonly Type[] pluggableEditorConstructorParameterTypes = new Type[] { typeof(Bindable<Editor>) };

        private static readonly Dictionary<Type, PluggableEditorConstructor> constructors = new Dictionary<Type, PluggableEditorConstructor>();

        static EditorFactory()
        {
            // TODO: This implementation is too low-level to be exposed in such a high-level application,
            // must be abstracted in another reflection-related place exposing this functionality
            var types = typeof(EditorFactory).Assembly.GetTypes().Where(type => typeof(BasePluggableEditor).IsAssignableFrom(type));
            foreach (var t in types)
                constructors.Add(t, GetDelegateFromConstructor(t.GetConstructor(pluggableEditorConstructorParameterTypes)));
        }

        public static Editor NewBasicEditor(Level level, out BasicEditor basicEditor) => NewEditor(level, out basicEditor);

        #region NewEditor<params T[]>
        // Now imagine a place with better code
        // Where the code below would be much better:
        /*
         *  public static Editor NewEditor<params T[]>(Level level, params out T[] components)
         *      where T : BasePluggableEditor, new(Bindable<Editor>) // BasePluggableEditor should be full anyway
         *  {
         *      var editor = new Editor(level);
         *      var editorBindable = new Bindable<Editor>(editor);
         *      for (int i = 0; i < T.Length; i++)
         *          components[i] = new T[i](editorBindable);
         *      return editor;
         *  }
         */
        // This may sadly not be easily abstracted, the most realistic scenario is adding the type argument array concept

        // Thankfully the following code was autogenerated
        /// <summary>Initializes a new <seealso cref="Editor"/> and plugs a component into that instance.</summary>
        /// <typeparam name="T">The type of the <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <param name="level">The level that the <seealso cref="Editor"/> will be editing.</param>
        /// <param name="component">The initialized instance of the component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        public static Editor NewEditor<T>(Level level, out T component)
            where T : BasePluggableEditor
        {
            var editor = new Editor(level);
            var editorBindable = new Bindable<Editor>(editor);
            component = InitializeEditorComponent<T>(editorBindable);
            return editor;
        }
        /// <summary>Initializes a new <seealso cref="Editor"/> and plugs a component into that instance.</summary>
        /// <typeparam name="T">The type of the <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <param name="levelBindable">The level that the <seealso cref="Editor"/> will be editing.</param>
        /// <param name="component">The initialized instance of the component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        public static Editor NewEditor<T>(Bindable<Level> levelBindable, out T component)
            where T : BasePluggableEditor
        {
            var editor = new Editor(levelBindable);
            var editorBindable = new Bindable<Editor>(editor);
            component = InitializeEditorComponent<T>(editorBindable);
            return editor;
        }
        /// <summary>Initializes a new <seealso cref="Editor"/> and plugs 2 components into that instance.</summary>
        /// <typeparam name="T1">The type of the 1st <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T2">The type of the 2nd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <param name="level">The level that the <seealso cref="Editor"/> will be editing.</param>
        /// <param name="component1">The initialized instance of the 1st component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component2">The initialized instance of the 2nd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        public static Editor NewEditor<T1, T2>(Level level, out T1 component1, out T2 component2)
            where T1 : BasePluggableEditor
            where T2 : BasePluggableEditor
        {
            var editor = new Editor(level);
            var editorBindable = new Bindable<Editor>(editor);
            component1 = InitializeEditorComponent<T1>(editorBindable);
            component2 = InitializeEditorComponent<T2>(editorBindable);
            return editor;
        }
        /// <summary>Initializes a new <seealso cref="Editor"/> and plugs 2 components into that instance.</summary>
        /// <typeparam name="T1">The type of the 1st <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T2">The type of the 2nd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <param name="levelBindable">The level that the <seealso cref="Editor"/> will be editing.</param>
        /// <param name="component1">The initialized instance of the 1st component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component2">The initialized instance of the 2nd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        public static Editor NewEditor<T1, T2>(Bindable<Level> levelBindable, out T1 component1, out T2 component2)
            where T1 : BasePluggableEditor
            where T2 : BasePluggableEditor
        {
            var editor = new Editor(levelBindable);
            var editorBindable = new Bindable<Editor>(editor);
            component1 = InitializeEditorComponent<T1>(editorBindable);
            component2 = InitializeEditorComponent<T2>(editorBindable);
            return editor;
        }
        /// <summary>Initializes a new <seealso cref="Editor"/> and plugs 3 components into that instance.</summary>
        /// <typeparam name="T1">The type of the 1st <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T2">The type of the 2nd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T3">The type of the 3rd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <param name="level">The level that the <seealso cref="Editor"/> will be editing.</param>
        /// <param name="component1">The initialized instance of the 1st component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component2">The initialized instance of the 2nd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component3">The initialized instance of the 3rd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        public static Editor NewEditor<T1, T2, T3>(Level level, out T1 component1, out T2 component2, out T3 component3)
            where T1 : BasePluggableEditor
            where T2 : BasePluggableEditor
            where T3 : BasePluggableEditor
        {
            var editor = new Editor(level);
            var editorBindable = new Bindable<Editor>(editor);
            component1 = InitializeEditorComponent<T1>(editorBindable);
            component2 = InitializeEditorComponent<T2>(editorBindable);
            component3 = InitializeEditorComponent<T3>(editorBindable);
            return editor;
        }
        /// <summary>Initializes a new <seealso cref="Editor"/> and plugs 3 components into that instance.</summary>
        /// <typeparam name="T1">The type of the 1st <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T2">The type of the 2nd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T3">The type of the 3rd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <param name="levelBindable">The level that the <seealso cref="Editor"/> will be editing.</param>
        /// <param name="component1">The initialized instance of the 1st component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component2">The initialized instance of the 2nd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component3">The initialized instance of the 3rd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        public static Editor NewEditor<T1, T2, T3>(Bindable<Level> levelBindable, out T1 component1, out T2 component2, out T3 component3)
            where T1 : BasePluggableEditor
            where T2 : BasePluggableEditor
            where T3 : BasePluggableEditor
        {
            var editor = new Editor(levelBindable);
            var editorBindable = new Bindable<Editor>(editor);
            component1 = InitializeEditorComponent<T1>(editorBindable);
            component2 = InitializeEditorComponent<T2>(editorBindable);
            component3 = InitializeEditorComponent<T3>(editorBindable);
            return editor;
        }
        /// <summary>Initializes a new <seealso cref="Editor"/> and plugs 4 components into that instance.</summary>
        /// <typeparam name="T1">The type of the 1st <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T2">The type of the 2nd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T3">The type of the 3rd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T4">The type of the 4th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <param name="level">The level that the <seealso cref="Editor"/> will be editing.</param>
        /// <param name="component1">The initialized instance of the 1st component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component2">The initialized instance of the 2nd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component3">The initialized instance of the 3rd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component4">The initialized instance of the 4th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        public static Editor NewEditor<T1, T2, T3, T4>(Level level, out T1 component1, out T2 component2, out T3 component3, out T4 component4)
            where T1 : BasePluggableEditor
            where T2 : BasePluggableEditor
            where T3 : BasePluggableEditor
            where T4 : BasePluggableEditor
        {
            var editor = new Editor(level);
            var editorBindable = new Bindable<Editor>(editor);
            component1 = InitializeEditorComponent<T1>(editorBindable);
            component2 = InitializeEditorComponent<T2>(editorBindable);
            component3 = InitializeEditorComponent<T3>(editorBindable);
            component4 = InitializeEditorComponent<T4>(editorBindable);
            return editor;
        }
        /// <summary>Initializes a new <seealso cref="Editor"/> and plugs 4 components into that instance.</summary>
        /// <typeparam name="T1">The type of the 1st <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T2">The type of the 2nd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T3">The type of the 3rd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T4">The type of the 4th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <param name="levelBindable">The level that the <seealso cref="Editor"/> will be editing.</param>
        /// <param name="component1">The initialized instance of the 1st component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component2">The initialized instance of the 2nd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component3">The initialized instance of the 3rd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component4">The initialized instance of the 4th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        public static Editor NewEditor<T1, T2, T3, T4>(Bindable<Level> levelBindable, out T1 component1, out T2 component2, out T3 component3, out T4 component4)
            where T1 : BasePluggableEditor
            where T2 : BasePluggableEditor
            where T3 : BasePluggableEditor
            where T4 : BasePluggableEditor
        {
            var editor = new Editor(levelBindable);
            var editorBindable = new Bindable<Editor>(editor);
            component1 = InitializeEditorComponent<T1>(editorBindable);
            component2 = InitializeEditorComponent<T2>(editorBindable);
            component3 = InitializeEditorComponent<T3>(editorBindable);
            component4 = InitializeEditorComponent<T4>(editorBindable);
            return editor;
        }
        /// <summary>Initializes a new <seealso cref="Editor"/> and plugs 5 components into that instance.</summary>
        /// <typeparam name="T1">The type of the 1st <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T2">The type of the 2nd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T3">The type of the 3rd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T4">The type of the 4th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T5">The type of the 5th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <param name="level">The level that the <seealso cref="Editor"/> will be editing.</param>
        /// <param name="component1">The initialized instance of the 1st component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component2">The initialized instance of the 2nd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component3">The initialized instance of the 3rd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component4">The initialized instance of the 4th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component5">The initialized instance of the 5th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        public static Editor NewEditor<T1, T2, T3, T4, T5>(Level level, out T1 component1, out T2 component2, out T3 component3, out T4 component4, out T5 component5)
            where T1 : BasePluggableEditor
            where T2 : BasePluggableEditor
            where T3 : BasePluggableEditor
            where T4 : BasePluggableEditor
            where T5 : BasePluggableEditor
        {
            var editor = new Editor(level);
            var editorBindable = new Bindable<Editor>(editor);
            component1 = InitializeEditorComponent<T1>(editorBindable);
            component2 = InitializeEditorComponent<T2>(editorBindable);
            component3 = InitializeEditorComponent<T3>(editorBindable);
            component4 = InitializeEditorComponent<T4>(editorBindable);
            component5 = InitializeEditorComponent<T5>(editorBindable);
            return editor;
        }
        /// <summary>Initializes a new <seealso cref="Editor"/> and plugs 5 components into that instance.</summary>
        /// <typeparam name="T1">The type of the 1st <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T2">The type of the 2nd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T3">The type of the 3rd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T4">The type of the 4th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T5">The type of the 5th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <param name="levelBindable">The level that the <seealso cref="Editor"/> will be editing.</param>
        /// <param name="component1">The initialized instance of the 1st component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component2">The initialized instance of the 2nd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component3">The initialized instance of the 3rd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component4">The initialized instance of the 4th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component5">The initialized instance of the 5th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        public static Editor NewEditor<T1, T2, T3, T4, T5>(Bindable<Level> levelBindable, out T1 component1, out T2 component2, out T3 component3, out T4 component4, out T5 component5)
            where T1 : BasePluggableEditor
            where T2 : BasePluggableEditor
            where T3 : BasePluggableEditor
            where T4 : BasePluggableEditor
            where T5 : BasePluggableEditor
        {
            var editor = new Editor(levelBindable);
            var editorBindable = new Bindable<Editor>(editor);
            component1 = InitializeEditorComponent<T1>(editorBindable);
            component2 = InitializeEditorComponent<T2>(editorBindable);
            component3 = InitializeEditorComponent<T3>(editorBindable);
            component4 = InitializeEditorComponent<T4>(editorBindable);
            component5 = InitializeEditorComponent<T5>(editorBindable);
            return editor;
        }
        /// <summary>Initializes a new <seealso cref="Editor"/> and plugs 6 components into that instance.</summary>
        /// <typeparam name="T1">The type of the 1st <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T2">The type of the 2nd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T3">The type of the 3rd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T4">The type of the 4th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T5">The type of the 5th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T6">The type of the 6th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <param name="level">The level that the <seealso cref="Editor"/> will be editing.</param>
        /// <param name="component1">The initialized instance of the 1st component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component2">The initialized instance of the 2nd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component3">The initialized instance of the 3rd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component4">The initialized instance of the 4th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component5">The initialized instance of the 5th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component6">The initialized instance of the 6th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        public static Editor NewEditor<T1, T2, T3, T4, T5, T6>(Level level, out T1 component1, out T2 component2, out T3 component3, out T4 component4, out T5 component5, out T6 component6)
            where T1 : BasePluggableEditor
            where T2 : BasePluggableEditor
            where T3 : BasePluggableEditor
            where T4 : BasePluggableEditor
            where T5 : BasePluggableEditor
            where T6 : BasePluggableEditor
        {
            var editor = new Editor(level);
            var editorBindable = new Bindable<Editor>(editor);
            component1 = InitializeEditorComponent<T1>(editorBindable);
            component2 = InitializeEditorComponent<T2>(editorBindable);
            component3 = InitializeEditorComponent<T3>(editorBindable);
            component4 = InitializeEditorComponent<T4>(editorBindable);
            component5 = InitializeEditorComponent<T5>(editorBindable);
            component6 = InitializeEditorComponent<T6>(editorBindable);
            return editor;
        }
        /// <summary>Initializes a new <seealso cref="Editor"/> and plugs 6 components into that instance.</summary>
        /// <typeparam name="T1">The type of the 1st <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T2">The type of the 2nd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T3">The type of the 3rd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T4">The type of the 4th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T5">The type of the 5th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T6">The type of the 6th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <param name="levelBindable">The level that the <seealso cref="Editor"/> will be editing.</param>
        /// <param name="component1">The initialized instance of the 1st component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component2">The initialized instance of the 2nd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component3">The initialized instance of the 3rd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component4">The initialized instance of the 4th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component5">The initialized instance of the 5th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component6">The initialized instance of the 6th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        public static Editor NewEditor<T1, T2, T3, T4, T5, T6>(Bindable<Level> levelBindable, out T1 component1, out T2 component2, out T3 component3, out T4 component4, out T5 component5, out T6 component6)
            where T1 : BasePluggableEditor
            where T2 : BasePluggableEditor
            where T3 : BasePluggableEditor
            where T4 : BasePluggableEditor
            where T5 : BasePluggableEditor
            where T6 : BasePluggableEditor
        {
            var editor = new Editor(levelBindable);
            var editorBindable = new Bindable<Editor>(editor);
            component1 = InitializeEditorComponent<T1>(editorBindable);
            component2 = InitializeEditorComponent<T2>(editorBindable);
            component3 = InitializeEditorComponent<T3>(editorBindable);
            component4 = InitializeEditorComponent<T4>(editorBindable);
            component5 = InitializeEditorComponent<T5>(editorBindable);
            component6 = InitializeEditorComponent<T6>(editorBindable);
            return editor;
        }
        /// <summary>Initializes a new <seealso cref="Editor"/> and plugs 7 components into that instance.</summary>
        /// <typeparam name="T1">The type of the 1st <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T2">The type of the 2nd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T3">The type of the 3rd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T4">The type of the 4th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T5">The type of the 5th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T6">The type of the 6th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T7">The type of the 7th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <param name="level">The level that the <seealso cref="Editor"/> will be editing.</param>
        /// <param name="component1">The initialized instance of the 1st component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component2">The initialized instance of the 2nd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component3">The initialized instance of the 3rd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component4">The initialized instance of the 4th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component5">The initialized instance of the 5th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component6">The initialized instance of the 6th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component7">The initialized instance of the 7th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        public static Editor NewEditor<T1, T2, T3, T4, T5, T6, T7>(Level level, out T1 component1, out T2 component2, out T3 component3, out T4 component4, out T5 component5, out T6 component6, out T7 component7)
            where T1 : BasePluggableEditor
            where T2 : BasePluggableEditor
            where T3 : BasePluggableEditor
            where T4 : BasePluggableEditor
            where T5 : BasePluggableEditor
            where T6 : BasePluggableEditor
            where T7 : BasePluggableEditor
        {
            var editor = new Editor(level);
            var editorBindable = new Bindable<Editor>(editor);
            component1 = InitializeEditorComponent<T1>(editorBindable);
            component2 = InitializeEditorComponent<T2>(editorBindable);
            component3 = InitializeEditorComponent<T3>(editorBindable);
            component4 = InitializeEditorComponent<T4>(editorBindable);
            component5 = InitializeEditorComponent<T5>(editorBindable);
            component6 = InitializeEditorComponent<T6>(editorBindable);
            component7 = InitializeEditorComponent<T7>(editorBindable);
            return editor;
        }
        /// <summary>Initializes a new <seealso cref="Editor"/> and plugs 7 components into that instance.</summary>
        /// <typeparam name="T1">The type of the 1st <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T2">The type of the 2nd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T3">The type of the 3rd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T4">The type of the 4th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T5">The type of the 5th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T6">The type of the 6th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T7">The type of the 7th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <param name="levelBindable">The level that the <seealso cref="Editor"/> will be editing.</param>
        /// <param name="component1">The initialized instance of the 1st component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component2">The initialized instance of the 2nd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component3">The initialized instance of the 3rd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component4">The initialized instance of the 4th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component5">The initialized instance of the 5th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component6">The initialized instance of the 6th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component7">The initialized instance of the 7th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        public static Editor NewEditor<T1, T2, T3, T4, T5, T6, T7>(Bindable<Level> levelBindable, out T1 component1, out T2 component2, out T3 component3, out T4 component4, out T5 component5, out T6 component6, out T7 component7)
            where T1 : BasePluggableEditor
            where T2 : BasePluggableEditor
            where T3 : BasePluggableEditor
            where T4 : BasePluggableEditor
            where T5 : BasePluggableEditor
            where T6 : BasePluggableEditor
            where T7 : BasePluggableEditor
        {
            var editor = new Editor(levelBindable);
            var editorBindable = new Bindable<Editor>(editor);
            component1 = InitializeEditorComponent<T1>(editorBindable);
            component2 = InitializeEditorComponent<T2>(editorBindable);
            component3 = InitializeEditorComponent<T3>(editorBindable);
            component4 = InitializeEditorComponent<T4>(editorBindable);
            component5 = InitializeEditorComponent<T5>(editorBindable);
            component6 = InitializeEditorComponent<T6>(editorBindable);
            component7 = InitializeEditorComponent<T7>(editorBindable);
            return editor;
        }
        /// <summary>Initializes a new <seealso cref="Editor"/> and plugs 8 components into that instance.</summary>
        /// <typeparam name="T1">The type of the 1st <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T2">The type of the 2nd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T3">The type of the 3rd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T4">The type of the 4th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T5">The type of the 5th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T6">The type of the 6th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T7">The type of the 7th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T8">The type of the 8th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <param name="level">The level that the <seealso cref="Editor"/> will be editing.</param>
        /// <param name="component1">The initialized instance of the 1st component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component2">The initialized instance of the 2nd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component3">The initialized instance of the 3rd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component4">The initialized instance of the 4th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component5">The initialized instance of the 5th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component6">The initialized instance of the 6th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component7">The initialized instance of the 7th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component8">The initialized instance of the 8th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        public static Editor NewEditor<T1, T2, T3, T4, T5, T6, T7, T8>(Level level, out T1 component1, out T2 component2, out T3 component3, out T4 component4, out T5 component5, out T6 component6, out T7 component7, out T8 component8)
            where T1 : BasePluggableEditor
            where T2 : BasePluggableEditor
            where T3 : BasePluggableEditor
            where T4 : BasePluggableEditor
            where T5 : BasePluggableEditor
            where T6 : BasePluggableEditor
            where T7 : BasePluggableEditor
            where T8 : BasePluggableEditor
        {
            var editor = new Editor(level);
            var editorBindable = new Bindable<Editor>(editor);
            component1 = InitializeEditorComponent<T1>(editorBindable);
            component2 = InitializeEditorComponent<T2>(editorBindable);
            component3 = InitializeEditorComponent<T3>(editorBindable);
            component4 = InitializeEditorComponent<T4>(editorBindable);
            component5 = InitializeEditorComponent<T5>(editorBindable);
            component6 = InitializeEditorComponent<T6>(editorBindable);
            component7 = InitializeEditorComponent<T7>(editorBindable);
            component8 = InitializeEditorComponent<T8>(editorBindable);
            return editor;
        }
        /// <summary>Initializes a new <seealso cref="Editor"/> and plugs 8 components into that instance.</summary>
        /// <typeparam name="T1">The type of the 1st <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T2">The type of the 2nd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T3">The type of the 3rd <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T4">The type of the 4th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T5">The type of the 5th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T6">The type of the 6th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T7">The type of the 7th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <typeparam name="T8">The type of the 8th <seealso cref="BasePluggableEditor"/> component.</typeparam>
        /// <param name="levelBindable">The level that the <seealso cref="Editor"/> will be editing.</param>
        /// <param name="component1">The initialized instance of the 1st component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component2">The initialized instance of the 2nd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component3">The initialized instance of the 3rd component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component4">The initialized instance of the 4th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component5">The initialized instance of the 5th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component6">The initialized instance of the 6th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component7">The initialized instance of the 7th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        /// <param name="component8">The initialized instance of the 8th component that will be plugged to the <seealso cref="Editor"/>.</typeparam>
        public static Editor NewEditor<T1, T2, T3, T4, T5, T6, T7, T8>(Bindable<Level> levelBindable, out T1 component1, out T2 component2, out T3 component3, out T4 component4, out T5 component5, out T6 component6, out T7 component7, out T8 component8)
            where T1 : BasePluggableEditor
            where T2 : BasePluggableEditor
            where T3 : BasePluggableEditor
            where T4 : BasePluggableEditor
            where T5 : BasePluggableEditor
            where T6 : BasePluggableEditor
            where T7 : BasePluggableEditor
            where T8 : BasePluggableEditor
        {
            var editor = new Editor(levelBindable);
            var editorBindable = new Bindable<Editor>(editor);
            component1 = InitializeEditorComponent<T1>(editorBindable);
            component2 = InitializeEditorComponent<T2>(editorBindable);
            component3 = InitializeEditorComponent<T3>(editorBindable);
            component4 = InitializeEditorComponent<T4>(editorBindable);
            component5 = InitializeEditorComponent<T5>(editorBindable);
            component6 = InitializeEditorComponent<T6>(editorBindable);
            component7 = InitializeEditorComponent<T7>(editorBindable);
            component8 = InitializeEditorComponent<T8>(editorBindable);
            return editor;
        }
        #endregion

        public static T InitializeEditorComponent<T>(Bindable<Editor> editorBindable)
            where T : BasePluggableEditor
        {
            return typeof(T).GetConstructor(pluggableEditorConstructorParameterTypes).Invoke(new object[] { editorBindable }) as T;
        }

        // TOOD: Make a general function to allow that functionality
        private static PluggableEditorConstructor GetDelegateFromConstructor(ConstructorInfo info)
        {
            return editorBindable => info.Invoke(new object[] { editorBindable }) as BasePluggableEditor;
        }

        protected delegate BasePluggableEditor PluggableEditorConstructor(Bindable<Editor> editorBindable);
    }
}
