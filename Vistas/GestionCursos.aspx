<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel.Master" AutoEventWireup="true" CodeBehind="GestionCursos.aspx.cs" Inherits="Vistas.GestionCursos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main class="flex-1 p-8">
            <div class="w-full max-w-7xl mx-auto flex flex-col gap-6">
                <!-- header -->
                <header class="flex flex-wrap justify-between items-center gap-4">
                    <h2 class="text-slate-900 dark:text-white text-3xl font-bold leading-tight">Gestión de Cursos</h2>
                    <button class="flex min-w-[84px] cursor-pointer items-center justify-center gap-2 overflow-hidden rounded-lg h-10 px-4 bg-primary text-white text-sm font-bold shadow-sm hover:bg-primary/90 transition-colors">
                        <span class="material-symbols-outlined text-xl">add</span>
                        <span class="truncate">Agregar Nuevo Curso</span>
                    </button>
                </header>
                <!-- searchBar y filters -->
                <div class="bg-white dark:bg-slate-900/50 p-4 rounded-xl border border-slate-200 dark:border-slate-800">
                    <label class="flex flex-col min-w-40 h-12 w-full">
                        <div class="flex w-full flex-1 items-stretch rounded-lg h-full">
                            <div class="text-slate-500 dark:text-slate-400 flex bg-slate-100 dark:bg-slate-800 items-center justify-center pl-4 rounded-l-lg">
                                <span class="material-symbols-outlined">search</span>
                            </div>
                            <input class="form-input flex w-full min-w-0 flex-1 resize-none overflow-hidden rounded-r-lg text-slate-900 dark:text-white focus:outline-0 focus:ring-2 focus:ring-primary/50 border-none bg-slate-100 dark:bg-slate-800 h-full placeholder:text-slate-500 dark:placeholder:text-slate-400 px-4 text-base font-normal" placeholder="Buscar cursos por título, categoría..." value="" />
                        </div>
                    </label>
                </div>
                <!-- tabla -->
                <div class="flex overflow-hidden rounded-xl border border-slate-200 dark:border-slate-800 bg-white dark:bg-slate-900/50">
                    <table class="w-full text-left">
                        <thead class="bg-slate-50 dark:bg-slate-900">
                            <tr class="text-slate-600 dark:text-slate-400">
                                <th class="px-4 py-3 text-sm font-medium">Título</th>
                                <th class="px-4 py-3 text-sm font-medium">Categoría</th>
                                <th class="px-4 py-3 text-sm font-medium">Precio</th>
                                <th class="px-4 py-3 text-sm font-medium text-center">Publicado</th>
                                <th class="px-4 py-3 text-sm font-medium">Estado</th>
                                <th class="px-4 py-3 text-sm font-medium">Acciones</th>
                            </tr>
                        </thead>
                        <tbody class="divide-y divide-slate-200 dark:divide-slate-800">
                            <tr class="hover:bg-slate-50 dark:hover:bg-slate-800/50 transition-colors">
                                <td class="px-4 py-3 text-sm font-medium text-slate-900 dark:text-white">Introducción a la Programación</td>
                                <td class="px-4 py-3 text-sm text-slate-500 dark:text-slate-400">Desarrollo Web</td>
                                <td class="px-4 py-3 text-sm text-slate-500 dark:text-slate-400">$49.99</td>
                                <td class="px-4 py-3 text-center">
                                    <label class="relative inline-flex items-center cursor-pointer">
                                        <input checked="" class="sr-only peer" type="checkbox" value="" />
                                        <div class="w-11 h-6 bg-slate-200 dark:bg-slate-700 rounded-full peer peer-focus:ring-2 peer-focus:ring-primary/50 peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-0.5 after:left-[2px] after:bg-white after:border-slate-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-primary"></div>
                                    </label>
                                </td>
                                <td class="px-4 py-3">
                                    <span class="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium bg-green-100 dark:bg-green-900/50 text-green-800 dark:text-green-300">Activo</span>
                                </td>
                                <td class="px-4 py-3">
                                    <div class="flex items-center gap-2">
                                        <button class="p-2 text-slate-500 dark:text-slate-400 hover:bg-primary/10 hover:text-primary rounded-full transition-colors" data-alt="Edit course"><span class="material-symbols-outlined text-xl">edit</span></button>
                                        <button class="p-2 text-slate-500 dark:text-slate-400 hover:bg-primary/10 hover:text-primary rounded-full transition-colors" data-alt="Archive course"><span class="material-symbols-outlined text-xl">archive</span></button>
                                        <button class="p-2 text-slate-500 dark:text-slate-400 hover:bg-primary/10 hover:text-primary rounded-full transition-colors" data-alt="Manage modules"><span class="material-symbols-outlined text-xl">view_module</span></button>
                                    </div>
                                </td>
                            </tr>
                            <tr class="hover:bg-slate-50 dark:hover:bg-slate-800/50 transition-colors">
                                <td class="px-4 py-3 text-sm font-medium text-slate-900 dark:text-white">Marketing Digital Avanzado</td>
                                <td class="px-4 py-3 text-sm text-slate-500 dark:text-slate-400">Marketing</td>
                                <td class="px-4 py-3 text-sm text-slate-500 dark:text-slate-400">$79.99</td>
                                <td class="px-4 py-3 text-center">
                                    <label class="relative inline-flex items-center cursor-pointer">
                                        <input checked="" class="sr-only peer" type="checkbox" value="" />
                                        <div class="w-11 h-6 bg-slate-200 dark:bg-slate-700 rounded-full peer peer-focus:ring-2 peer-focus:ring-primary/50 peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-0.5 after:left-[2px] after:bg-white after:border-slate-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-primary"></div>
                                    </label>
                                </td>
                                <td class="px-4 py-3">
                                    <span class="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium bg-green-100 dark:bg-green-900/50 text-green-800 dark:text-green-300">Activo</span>
                                </td>
                                <td class="px-4 py-3">
                                    <div class="flex items-center gap-2">
                                        <button class="p-2 text-slate-500 dark:text-slate-400 hover:bg-primary/10 hover:text-primary rounded-full transition-colors" data-alt="Edit course"><span class="material-symbols-outlined text-xl">edit</span></button>
                                        <button class="p-2 text-slate-500 dark:text-slate-400 hover:bg-primary/10 hover:text-primary rounded-full transition-colors" data-alt="Archive course"><span class="material-symbols-outlined text-xl">archive</span></button>
                                        <button class="p-2 text-slate-500 dark:text-slate-400 hover:bg-primary/10 hover:text-primary rounded-full transition-colors" data-alt="Manage modules"><span class="material-symbols-outlined text-xl">view_module</span></button>
                                    </div>
                                </td>
                            </tr>
                            <tr class="hover:bg-slate-50 dark:hover:bg-slate-800/50 transition-colors">
                                <td class="px-4 py-3 text-sm font-medium text-slate-900 dark:text-white">Fundamentos del Diseño UI/UX</td>
                                <td class="px-4 py-3 text-sm text-slate-500 dark:text-slate-400">Diseño</td>
                                <td class="px-4 py-3 text-sm text-slate-500 dark:text-slate-400">$60.00</td>
                                <td class="px-4 py-3 text-center">
                                    <label class="relative inline-flex items-center cursor-pointer">
                                        <input class="sr-only peer" type="checkbox" value="" />
                                        <div class="w-11 h-6 bg-slate-200 dark:bg-slate-700 rounded-full peer peer-focus:ring-2 peer-focus:ring-primary/50 peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-0.5 after:left-[2px] after:bg-white after:border-slate-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-primary"></div>
                                    </label>
                                </td>
                                <td class="px-4 py-3">
                                    <span class="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium bg-green-100 dark:bg-green-900/50 text-green-800 dark:text-green-300">Activo</span>
                                </td>
                                <td class="px-4 py-3">
                                    <div class="flex items-center gap-2">
                                        <button class="p-2 text-slate-500 dark:text-slate-400 hover:bg-primary/10 hover:text-primary rounded-full transition-colors" data-alt="Edit course"><span class="material-symbols-outlined text-xl">edit</span></button>
                                        <button class="p-2 text-slate-500 dark:text-slate-400 hover:bg-primary/10 hover:text-primary rounded-full transition-colors" data-alt="Archive course"><span class="material-symbols-outlined text-xl">archive</span></button>
                                        <button class="p-2 text-slate-500 dark:text-slate-400 hover:bg-primary/10 hover:text-primary rounded-full transition-colors" data-alt="Manage modules"><span class="material-symbols-outlined text-xl">view_module</span></button>
                                    </div>
                                </td>
                            </tr>
                            <tr class="hover:bg-slate-50 dark:hover:bg-slate-800/50 transition-colors">
                                <td class="px-4 py-3 text-sm font-medium text-slate-900 dark:text-white">Contabilidad Básica (2023)</td>
                                <td class="px-4 py-3 text-sm text-slate-500 dark:text-slate-400">Negocios</td>
                                <td class="px-4 py-3 text-sm text-slate-500 dark:text-slate-400">$99.99</td>
                                <td class="px-4 py-3 text-center">
                                    <label class="relative inline-flex items-center cursor-pointer">
                                        <input checked="" class="sr-only peer" type="checkbox" value="" />
                                        <div class="w-11 h-6 bg-slate-200 dark:bg-slate-700 rounded-full peer peer-focus:ring-2 peer-focus:ring-primary/50 peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-0.5 after:left-[2px] after:bg-white after:border-slate-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-primary"></div>
                                    </label>
                                </td>
                                <td class="px-4 py-3">
                                    <span class="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium bg-slate-100 dark:bg-slate-800 text-slate-600 dark:text-slate-300">Archivado</span>
                                </td>
                                <td class="px-4 py-3">
                                    <div class="flex items-center gap-2">
                                        <button class="p-2 text-slate-500 dark:text-slate-400 hover:bg-primary/10 hover:text-primary rounded-full transition-colors" data-alt="Edit course"><span class="material-symbols-outlined text-xl">edit</span></button>
                                        <button class="p-2 text-slate-500 dark:text-slate-400 hover:bg-primary/10 hover:text-primary rounded-full transition-colors" data-alt="Archive course"><span class="material-symbols-outlined text-xl">unarchive</span></button>
                                        <button class="p-2 text-slate-500 dark:text-slate-400 hover:bg-primary/10 hover:text-primary rounded-full transition-colors" data-alt="Manage modules"><span class="material-symbols-outlined text-xl">view_module</span></button>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <!-- paginacion -->
                <nav aria-label="Pagination" class="flex items-center justify-center p-4">
                    <a class="flex size-10 items-center justify-center rounded-lg text-slate-500 dark:text-slate-400 hover:bg-slate-100 dark:hover:bg-slate-800 transition-colors" href="#">
                        <span class="material-symbols-outlined text-xl">chevron_left</span>
                    </a>
                    <a class="text-sm font-bold flex size-10 items-center justify-center text-white bg-primary rounded-lg mx-1" href="#">1</a>
                    <a class="text-sm font-medium flex size-10 items-center justify-center text-slate-600 dark:text-slate-300 hover:bg-slate-100 dark:hover:bg-slate-800 rounded-lg mx-1 transition-colors" href="#">2</a>
                    <a class="text-sm font-medium flex size-10 items-center justify-center text-slate-600 dark:text-slate-300 hover:bg-slate-100 dark:hover:bg-slate-800 rounded-lg mx-1 transition-colors" href="#">3</a>
                    <span class="text-sm font-medium flex size-10 items-center justify-center text-slate-500 dark:text-slate-400 mx-1">...</span>
                    <a class="text-sm font-medium flex size-10 items-center justify-center text-slate-600 dark:text-slate-300 hover:bg-slate-100 dark:hover:bg-slate-800 rounded-lg mx-1 transition-colors" href="#">8</a>
                    <a class="text-sm font-medium flex size-10 items-center justify-center text-slate-600 dark:text-slate-300 hover:bg-slate-100 dark:hover:bg-slate-800 rounded-lg mx-1 transition-colors" href="#">9</a>
                    <a class="text-sm font-medium flex size-10 items-center justify-center text-slate-600 dark:text-slate-300 hover:bg-slate-100 dark:hover:bg-slate-800 rounded-lg mx-1 transition-colors" href="#">10</a>
                    <a class="flex size-10 items-center justify-center rounded-lg text-slate-500 dark:text-slate-400 hover:bg-slate-100 dark:hover:bg-slate-800 transition-colors" href="#">
                        <span class="material-symbols-outlined text-xl">chevron_right</span>
                    </a>
                </nav>
            </div>
        </main>
</asp:Content>
