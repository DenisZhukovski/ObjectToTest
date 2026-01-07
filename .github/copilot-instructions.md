# GitHub Copilot Custom Instructions for ObjectToTest

## Project Overview
ObjectToTest is a C# library that provides extension methods to generate code for recreating object states, primarily for unit testing. It analyzes constructors, properties, and internal state to produce initialization code snippets. The project emphasizes clean code, testability, and follows Elegant Objects principles.

## Coding Conventions
- Use C# best practices and .NET conventions.
- Prefer extension methods for object state recreation.
- Ensure code is readable, maintainable, and well-documented.
- Write unit tests for new features and changes.
- Follow the structure: source code in `src/`, tests in `tests/`, documentation in `docs/`.
- Use descriptive names for classes, methods, and variables.
- Avoid unnecessary complexity; keep logic simple and focused.

## Contribution Guidelines
- Add or update unit tests for all changes. Don't use Mocking frameworks when writing unit tests.
- Document public APIs and extension methods.
- Reference diagrams in `docs/` for understanding algorithms and object relationships.
- Respect Elegant Objects principles where applicable.

## Copilot Usage
- Suggest code that helps generate initialization code for objects.
- Propose extension methods and helpers for object state analysis.
- Recommend improvements to code formatting and test coverage.
- When in doubt, prefer clarity and simplicity.

## Maintenance Note
**As the project evolves, update this file to reflect new conventions, features, or architectural changes.**

For more details, see the README.md and diagrams in the `docs/` folder.

