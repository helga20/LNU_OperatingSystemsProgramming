import tkinter as tk
from tkinter import filedialog

class TextEditor:
    def __init__(self, master):
        self.master = master
        master.title("Text Editor")

        self.text = tk.Text(master)
        self.text.pack(expand=True, fill="both")

        self.menu = tk.Menu(master)
        self.master.config(menu=self.menu)

        self.file_menu = tk.Menu(self.menu)
        self.menu.add_cascade(label="File", menu=self.file_menu)
        self.file_menu.add_command(label="Open", command=self.open_file)
        self.file_menu.add_command(label="Save", command=self.save_file)

        self.encoding_menu = tk.Menu(self.menu)
        self.menu.add_cascade(label="Encoding", menu=self.encoding_menu)
        self.encoding_var = tk.StringVar(value="UTF-8")
        self.encoding_menu.add_radiobutton(label="UTF-8", variable=self.encoding_var, value="UTF-8")
        self.encoding_menu.add_radiobutton(label="ASCII", variable=self.encoding_var, value="ASCII")
        self.encoding_menu.add_radiobutton(label="ISO-8859-1", variable=self.encoding_var, value="ISO-8859-1")

    def open_file(self):
        file_path = filedialog.askopenfilename()
        if file_path:
            with open(file_path, encoding=self.encoding_var.get()) as f:
                self.text.delete("1.0", tk.END)
                self.text.insert("1.0", f.read())

    def save_file(self):
        file_path = filedialog.asksaveasfilename()
        if file_path:
            with open(file_path, "w", encoding=self.encoding_var.get()) as f:
                f.write(self.text.get("1.0", tk.END))


if __name__ == '__main__':
    root = tk.Tk()
    editor = TextEditor(root)
    root.mainloop()
