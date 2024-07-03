# PDF Library Comparison and Selection

This repository provides a comparison of three PDF libraries: Aspose, PdfPig, and QuestPDF. It evaluates the pros and cons of each library and offers a recommendation based on the current requirements and feedback from Sirius.
Only these libraries were considered as they do not require external dependencies or unmanaged code and are easily to integrate and work with.

## Libraries Overview

### Aspose

#### Pros
- **No external dependencies**: Aspose does not require any external dependencies or unmanaged code.
- **Existing license**: Sirius already possesses a license for Aspose.
- **Html => PDF**: allows to generate pdf from HTML template.
- **Language support**: Aspose supports multiple languages. Do not require additional fonts for each language.

#### Cons
- **Stuck to specific version**: Due to the non-prolongation of the license, Sirius is limited to using a specific version of Aspose.
- **Performance**: Users have reported that Aspose is slow.
- **Quality**: Feedback indicates that the quality of the generated PDFs is not up to the mark.

### PdfPig

[PdfPig GitHub Repository](https://github.com/UglyToad/PdfPig)

#### Pros
- **Free and open source**: PdfPig is a free and open-source library, which means there are no licensing costs.
- **Active support and development**: The library is actively supported and developed, ensuring regular updates and improvements.

#### Cons
- **Low-level manipulation**: PdfPig requires low-level manipulation of PDF documents, such as specifying the coordinates of the text, which can be inconvenient compared to more user-friendly libraries.
- **Low-level manipulation**: Multi-language not supported by default. For each language, we need to import the font

### QuestPDF

[QuestPDF GitHub Repository](https://github.com/QuestPDF/QuestPDF?tab=readme-ov-file)

#### Pros
- **Modern and actively supported. Open-source**: QuestPDF is a modern, actively supported, and developed library.
- **100% managed code**: The library is entirely managed code, which simplifies integration and use.
- **Fluent interface**: QuestPDF allows for fluent and easy manipulation of PDF documents, making it very user-friendly.
- **Feature-rich**: It offers a lot of features that facilitate PDF creation and manipulation.
- **Language support**: QuestPdf supports multiple languages. Do not require additional fonts for each language.


#### Cons
- **License required**: QuestPDF requires a license for full use, which could involve additional costs.


### Key Takeaways
- **Aspose**: Existing license but stuck to a specific version, performance and quality issues. Supports HTML => PDF conversion. Multiple language support.
- **PdfPig**: Free, open-source, low-level manipulation required. Import additional fonts are required (e.g. Japanese, Korean, Chinese).
- **QuestPDF**: Modern, easy to use, requires a license, recommended for better quality and performance. Multiple language support.
